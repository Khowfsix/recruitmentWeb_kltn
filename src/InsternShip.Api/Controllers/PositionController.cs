using InsternShip.Data.ViewModels.Position;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class PositionController : BaseAPIController
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> AddPosition(PositionAddModel position)
        {
            var response = await _positionService.AddPosition(position);
            return response is not null ? Ok(response)
                                        : BadRequest(position);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions(Guid? departmentId)
        {
            List<PositionViewModel> response = await _positionService.GetAllPositions(departmentId);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPositionById(Guid positionId)
        {
            var response = await _positionService.GetPositionById(positionId);

            if (response is not null)
            {
                return Ok(response);
            }
            return Ok(null);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPositionByName(string positionName)
        {
            var response = await _positionService.GetPositionByName(positionName);
            return response is not null ? Ok(response) : NotFound(positionName);
        }

        [HttpDelete("{positionId:guid}")]
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> RemovePosition(Guid positionId)
        {
            var response = await _positionService.RemovePosition(positionId);
            return response is true ? Ok(true) : Ok("Not found");
        }

        [HttpPut("{positionId:guid}")]
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> UpdatePosition
        (PositionUpdateModel position, Guid positionId)
        {
            var response = await _positionService.UpdatePosition(position, positionId);
            if (response != true)
            {
                return Ok(false);
            }
            return Ok(true);
        }
    }
}