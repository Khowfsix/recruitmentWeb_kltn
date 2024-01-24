using InsternShip.Data.ViewModels.Room;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class RoomController : BaseAPIController
    {
        private readonly IRoomService _reportService;

        public RoomController(IRoomService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoom()
        {
            var reportList = await _reportService.GetAllRoom();
            return Ok(reportList);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveRoom(RoomAddModel request)
        {
            var reportList = await _reportService.SaveRoom(request);
            return Ok(reportList);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateRoom(RoomUpdateModel request, Guid id)
        {
            var reportList = await _reportService.UpdateRoom(request, id);
            return Ok(reportList);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var reportList = await _reportService.DeleteRoom(id);
            return Ok(reportList);
        }
    }
}