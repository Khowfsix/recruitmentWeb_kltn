using InsternShip.Data.ViewModels.Recruiter;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;
[Authorize]
public class RecruiterController : BaseAPIController
{
    private readonly IRecruiterService _recruiterService;

    public RecruiterController(IRecruiterService recruiterService)
    {
        _recruiterService = recruiterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecruiter(Guid? id)
    {
        if (id != null)
        {
            var data = await _recruiterService.GetRecruiterById((Guid)id);
            return data switch
            {
                null => NotFound(),
                _ => Ok(data)
            };
        }

        var reportList = await _recruiterService.GetAllRecruiter();
        return Ok(reportList);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SaveRecruiter(RecruiterAddModel request)
    {
        var response = await _recruiterService.SaveRecruiter(request);
        if (response != null)
        {
            return Ok(response);
        }
        return Ok("Can not create recruiter");
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRecruiter(RecruiterUpdateModel request, Guid id)
    {
        return await _recruiterService.UpdateRecruiter(request, id) ? Ok(true) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRecruiter(Guid id)
    {
        return await _recruiterService.DeleteRecruiter(id) ? Ok(true) : NotFound();
    }
}