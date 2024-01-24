using InsternShip.Data.ViewModels.Result;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class ResultController : BaseAPIController
    {
        private readonly IResultService _reportService;

        public ResultController(IResultService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResult()
        {
            var reportList = await _reportService.GetAllResult();
            return Ok(reportList);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveResult(ResultAddModel request)
        {
            var reportList = await _reportService.SaveResult(request);
            return Ok(reportList);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateResult(ResultUpdateModel request, Guid id)
        {
            var reportList = await _reportService.UpdateResult(request, id);
            return Ok(reportList);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteResult(Guid id)
        {
            var reportList = await _reportService.DeleteResult(id);
            return Ok(reportList);
        }
    }
}