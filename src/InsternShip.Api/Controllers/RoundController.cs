using InsternShip.Data.ViewModels.Round;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class RoundController : BaseAPIController
    {
        private readonly IRoundService _roundService;

        public RoundController(IRoundService roundService)
        {
            _roundService = roundService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRound(string? query, Guid? interviewId)
        {
            if (interviewId != null)
            {
                var roundlistOfInterview = await _roundService.GetRoundsOfInterview((Guid)interviewId);
                if (roundlistOfInterview == null)
                {
                    return Ok();
                }

                return Ok(roundlistOfInterview);
            }

            var roundlist = await _roundService.GetAllRounds(query);
            if (roundlist == null)
            {
                return Ok("Not found");
            }

            return Ok(roundlist);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveRound(RoundAddModel roundModel)
        {
            if (roundModel == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var roundlist = await _roundService.SaveRound(roundModel);
            return Ok(roundlist);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateRound(RoundUpdateModel roundModel, Guid id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var roundlist = await _roundService.UpdateRound(roundModel, id);
            return Ok(roundlist);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteRound(Guid id)
        {
            if (id != null)
            {
                var roundlist = await _roundService.DeleteRound(id);
                return Ok(roundlist);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}