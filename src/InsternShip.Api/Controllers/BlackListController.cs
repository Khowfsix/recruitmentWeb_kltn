using InsternShip.Data.ViewModels.BlackList;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize(Roles ="Admin,Recruiter,Interviewer")]
    public class BlackListController : BaseAPIController
    {
        private readonly IBlacklistService _blacklistService;

        public BlackListController(IBlacklistService blacListService)
        {
            _blacklistService = blacListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var response = await _blacklistService.GetAllBlackLists();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlackList(BlackListAddModel request)
        {
            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var response = await _blacklistService.SaveBlackList(request);
            return Ok(response);
        }

        [HttpDelete("{requestId:guid}")]
        public async Task<IActionResult> DeleteBlackList(Guid requestId)
        {
            if (requestId.Equals(Guid.Empty))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var response = await _blacklistService.DeleteBlackList(requestId);
            return Ok(response);
        }

        [HttpPut("{requestId:guid}")]
        public async Task<IActionResult> UpdateBlackList(BlackListUpdateModel request, Guid requestId)
        {
            if (request == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var response = await _blacklistService.UpdateBlackList(request, requestId);
            return Ok(response);
        }
    }
}