using InsternShip.Data.ViewModels.Application;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class ApplicationController : BaseAPIController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications(string? status, string? priority)
        {
            if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(priority))
            {
                var response = await _applicationService.GetAllApplications();
                return Ok(response);
            }
            else
            {
                var response = await _applicationService.GetApplicationsWithStatus(
                    status!,
                    priority!
                );
                return Ok(response);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetApplicationById(Guid id)
        {
            var response = await _applicationService.GetApplicationById(id);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles ="Recruiter")]
        public async Task<IActionResult> SaveApplication(ApplicationAddModel request)
        {
            if (request == null)
                return StatusCode(StatusCodes.Status400BadRequest);
            var response = await _applicationService.SaveApplication(request);
            return Ok(response);
        }

        [HttpPut("{requestId:guid}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> UpdateApplication(
            ApplicationUpdateModel request,
            Guid requestId
        )
        {
            if (request == null)
                return StatusCode(StatusCodes.Status400BadRequest);
            var response = await _applicationService.UpdateApplication(request, requestId);
            return Ok(response);
        }

        [HttpPut("[action]/{ApplicationId:guid}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> UpdateStatusApplication(
            Guid ApplicationId,
            string? Candidate_Status,
            string? Company_Status
        )
        {
            if (Candidate_Status == null && Company_Status == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            //var applicationNewStatus = await _applicationService.GetApplicationById(ApplicationId);

            //if (applicationNewStatus == null)
            //{
            //    return BadRequest();
            //}

            var response = await _applicationService.UpdateStatusApplication(ApplicationId, Candidate_Status, Company_Status);
            return Ok(response);
        }

        [HttpDelete("{applicationId:guid}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> DeleteApplication(Guid applicationId)
        {
            if (applicationId.Equals(Guid.Empty))
                return StatusCode(StatusCodes.Status400BadRequest);
            var response = await (_applicationService.DeleteApplication(applicationId));
            return Ok(response);
        }
    }
}