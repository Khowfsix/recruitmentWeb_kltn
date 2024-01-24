using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class JobInterviewHistoryController : BaseAPIController
    {
        private readonly IJobInterviewHistoryService _jobInterviewHistoryService;

        public JobInterviewHistoryController
        (IJobInterviewHistoryService jobInterviewHistoryService)
        {
            _jobInterviewHistoryService = jobInterviewHistoryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetApplicationHistory(Guid candidateId)
        {
            var response =
                await _jobInterviewHistoryService.GetApplicationHistory(candidateId);
            return response is not null ? Ok(response) : NotFound(candidateId);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCV(Guid cvId)
        {
            var response = await _jobInterviewHistoryService.GetCV(cvId);
            return response is not null ? Ok(response) : NotFound(cvId);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetInterviewerInformation(Guid applicationId)
        {
            var response =
                await _jobInterviewHistoryService.GetInterviewerInformation(applicationId);
            return response is not null ? Ok(response) : NotFound(applicationId);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPosition(Guid applicationId)
        {
            var response = await _jobInterviewHistoryService.GetPosition(applicationId);
            return response is not null ? Ok(response) : NotFound(applicationId);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomInfo(Guid applicationId)
        {
            var response =
                await _jobInterviewHistoryService.GetRoomInformation(applicationId);
            return response is not null ? Ok(response) : NotFound(applicationId);
        }
    }
}