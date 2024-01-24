using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;
[Authorize]
public class ItrsinterviewController : BaseAPIController
{
    private readonly IItrsinterviewService _itrsinterviewService;

    public ItrsinterviewController(IItrsinterviewService itrsinterviewService)
    {
        _itrsinterviewService = itrsinterviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItrsinterview(Guid? id)
    {
        if (id != null)
        {
            var data = await _itrsinterviewService.GetItrsinterviewById((Guid)id);
            return data switch
            {
                null => Ok("Not found"),
                _ => Ok(data)
            };
        }
        var reportList = await _itrsinterviewService.GetAllItrsinterview();
        if (reportList == null)
        {
            return Ok("Not found");
        }
        return Ok(reportList);
    }

    [HttpPost]
    [Authorize(Roles = "Recruiter,Interviewer,Admin")]
    public async Task<IActionResult> SaveItrsinterview(ItrsinterviewAddModel request, Guid interviewerId)
    {
        if (request == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var response = await _itrsinterviewService.SaveItrsinterview(request, interviewerId);
        if (response == null)
        {
            return BadRequest();
        }

        if (response.ItrsinterviewId != Guid.Empty)
        {
            return Ok(response);
        }
        return StatusCode(StatusCodes.Status409Conflict);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Recruiter,Interviewer,Admin")]
    public async Task<IActionResult> UpdateItrsinterview(ItrsinterviewUpdateModel request, Guid id, Guid interviewerId)
    {
        return await _itrsinterviewService.UpdateItrsinterview(request, id, interviewerId) ? Ok(true) : Ok("Update is not success instead of Confict or Not found");
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Recruiter,Interviewer,Admin")]
    public async Task<IActionResult> DeleteItrsinterview(Guid id)
    {
        return await _itrsinterviewService.DeleteItrsinterview(id) ? Ok(true) : Ok("Not found");
    }
}