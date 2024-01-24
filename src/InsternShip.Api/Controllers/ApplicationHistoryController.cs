using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;
[Authorize]
public class ApplicationHistoryController : BaseAPIController
{
    private readonly IApplicationService _applicationService;

    public ApplicationHistoryController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("{candidateId:guid}")]
    public async Task<IActionResult> GetApplicationHistory(Guid candidateId)
    {
        var response = await _applicationService.GetApplicationHistory(candidateId);
        return Ok(response);
    }
}
