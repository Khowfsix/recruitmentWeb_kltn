using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.CandidateJoinEvent;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class CandidateJoinEventController : BaseAPIController
    {
        private readonly ICandidateJoinEventService _candidateJoinEventService;

        public CandidateJoinEventController(ICandidateJoinEventService candidateJoinEventService)
        {
            _candidateJoinEventService = candidateJoinEventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidateJoinEvents()
        {
            var response = await _candidateJoinEventService.GetAllCandidateJoinEvents();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCandidateJoinEvent(CandidateJoinEventAddModel request)
        {
            if (request == null)
            {
                return Ok("Not found");
            }
            var response = await _candidateJoinEventService.SaveCandidateJoinEvent(request);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(response);
        }

        [HttpDelete("{requestId:guid}")]
        public async Task<IActionResult> DeleteCandidateJoinEvent(Guid requestId)
        {
            if (requestId.Equals(Guid.Empty))
            {
                return Ok("Not found");
            }
            var response = await (_candidateJoinEventService.DeleteCandidateJoinEvent(requestId));
            return Ok(response);
        }

        [HttpPut("{requestId:guid}")]
        public async Task<IActionResult> UpdateCandidateJoinEvent(CandidateJoinEventUpdateModel request, Guid requestId)
        {
            if (request == null)
            {
                return Ok("Not found");
            }
            var response = await _candidateJoinEventService.UpdateCandidateJoinEvent(request, requestId);
            return Ok(response);
        }

        [HttpGet("[action]/{candidateJoinEventId:guid}")]
        public async Task<IActionResult> JoinEventDetail(Guid candidateJoinEventId)
        {
            var response = await _candidateJoinEventService.JoinEventDetail(candidateJoinEventId);
            return response is not null ? Ok(response) : NotFound();
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> GetCandidatesSortedByJoinEventCount()
        {
            var response = await _candidateJoinEventService.GetCandidatesSortedByJoinEventCount();
            return response is not null ? Ok(response) : NotFound();
        }

    }
}