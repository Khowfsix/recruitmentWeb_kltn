using InsternShip.Data.ViewModels.SuccessfulCadidate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class SuccessfulCadidateController : BaseAPIController
    {
        private readonly ISuccessfulCandidateService _successfulCandidateService;

        public SuccessfulCadidateController(ISuccessfulCandidateService successfulCandidateService)
        {
            _successfulCandidateService = successfulCandidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSCs(string? query)
        {
            var SCsList = await _successfulCandidateService.GetAllSuccessfulCadidates(query);
            if (SCsList == null)
            {
                return Ok("Not found");
            }

            return Ok(SCsList);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveSC(SuccessfulCadidateAddModel successfulCadidateModel)
        {
            if (successfulCadidateModel == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var SCsList = await _successfulCandidateService.SaveSuccessfulCadidate(successfulCadidateModel);
            return Ok(SCsList);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateCS(SuccessfulCadidateUpdateModel successfulCadidateModel, Guid id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var SCsList = await _successfulCandidateService.UpdateSuccessfulCadidate(successfulCadidateModel, id);
            return Ok(SCsList);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteCS(Guid id)
        {
            if (id != null)
            {
                var SCsList = await _successfulCandidateService.DeleteSuccessfulCadidate(id);
                return Ok(SCsList);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}