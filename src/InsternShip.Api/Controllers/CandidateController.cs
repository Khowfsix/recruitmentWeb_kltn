using InsternShip.Data.ViewModels.Candidate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class CandidateController : BaseAPIController
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var response = await _candidateService.GetAllCandidates();
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SaveCandidate(CandidateAddModel request)
        {
            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var response = await _candidateService.SaveCandidate(request);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{requestId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCandidate(Guid requestId)
        {
            if (requestId.Equals(Guid.Empty))
            {
                return BadRequest();
            }
            var response = await _candidateService.DeleteCandidate(requestId);
            if (response == true)
            {
                return Ok(true);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{requestId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCandidate(CandidateUpdateModel request, Guid requestId)
        {
            //if (request.CandidateId != requestId)
            //{
            //    return BadRequest();
            //}

            var response = await _candidateService.UpdateCandidate(request, requestId);
            if (response == true)
            {
                return Ok(true);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{candidateId:guid}")]
        public async Task<IActionResult> GetProfiles(Guid candidateId)
        {
            //var response = await _candidateService.GetProfile(candidateId);
            var response = await _candidateService.FindById(candidateId);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}