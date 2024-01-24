using InsternShip.Data.ViewModels.Interview;
using InsternShip.Data.ViewModels.Round;
using InsternShip.Service;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;
[Authorize]
public class InterviewController : BaseAPIController
{
    private readonly IInterviewService _interviewService;
    private readonly IItrsinterviewService _itrsinterviewService;
    private readonly IRoundService _roundService;

    #region comment
    //public InterviewController(IInterviewService interviewService,
    //    IItrsinterviewService itrsinterviewService,
    //    IRoundService roundService)
    //{
    //    _interviewService = interviewService;
    //    _itrsinterviewService = itrsinterviewService;
    //    _roundService = roundService;
    //}

    /// <summary>
    /// For unit testing
    /// </summary>
    /// <param name="interviewService"></param>
    /// <param name="itrsinterviewService"></param>
    /// 

    #endregion
    public InterviewController(
        IInterviewService interviewService,
        IItrsinterviewService itrsinterviewService)
    {
        _interviewService = interviewService;
        _itrsinterviewService = itrsinterviewService;
        _roundService = null!;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInterview(Guid? id, string? status)
    {
        // Get by id
        if (id != null)
        {
            var data = await _interviewService.GetInterviewById((Guid)id);
            return data switch
            {
                null => Ok("Not found"),
                _ => Ok(data)
            };
        }

        // Get by Interviewer
        if (status == null)
        {
            //var interviewByStatus = await _interviewService.GetAllInterviewByStatus(status);
            //if (interviewByStatus == null)
            //{
            //    return Ok();
            //}
            //return Ok(interviewByStatus);
            status = "";
        }

        var reportList = await _interviewService.GetAllInterview(status);
        if (reportList == null)
        {
            return Ok();
        }
        return Ok(reportList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByInterviewer(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByInterviewer(requestId);
        if (dataList == null)
        {
            return Ok();
        }
        return Ok(dataList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByPositon(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByPositon(requestId);
        if (dataList == null)
        {
            return Ok();
        }
        return Ok(dataList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByDepartment(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByDepartment(requestId);
        if (dataList == null)
        {
            return Ok();
        }
        return Ok(dataList);
    }

    [HttpPost("{applicationId:guid}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> SaveInterview(InterviewWithTimeAddModel request, Guid applicationId)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (request.Interview == null)
        {
            return BadRequest();
        }

        var responseITRS = await _itrsinterviewService.SaveItrsinterview(request.ITRS, request.Interview.InterviewerId);
        if (responseITRS! == null)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        request.Interview.ApplicationId = applicationId;
        request.Interview.ItrsinterviewId = responseITRS!.ItrsinterviewId;
        var responseInterview = await _interviewService.SaveInterview(request.Interview);

        if (responseInterview != null)
        {
            return Ok(responseInterview);
        }
        return StatusCode(StatusCodes.Status409Conflict);
    }

    [HttpPost("[action]/{id:guid}")]
    public async Task<IActionResult> PostQuestionInterviewResult(InterviewResultQuestionModel request)
    {
        var postQuestionIntoInterview = await _interviewService.PostQuestionIntoInterview(request);

        if (postQuestionIntoInterview == null)
        {
            return BadRequest(request);
        }
        else
        {
            return Ok(postQuestionIntoInterview);
        }
    }

    [HttpPut("{interviewId:guid}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> UpdateInterview([FromBody] InterviewUpdateModel request, Guid interviewId)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (request.InterviewId != interviewId)
        {
            return BadRequest();
        }

        //var responseITRS = await _itrsinterviewService.SaveItrsinterview(request.Itrsinterview!, request.InterviewerId);
        //if (responseITRS == null)
        //{
        //    return StatusCode(StatusCodes.Status409Conflict);
        //}

        //request.InterviewId = interviewId;
        //request.ItrsinterviewId = responseITRS.ItrsinterviewId;

        var responseInterview = await _interviewService.UpdateInterview(request, interviewId);

        //if (responseInterview == false)
        //{
        //    var rollback = await _itrsinterviewService.DeleteItrsinterview(responseITRS!.ItrsinterviewId);
        //    return StatusCode(StatusCodes.Status409Conflict);
        //}

        //var oldITRS = (Guid)request.ItrsinterviewId!;
        //var delITRS = await _itrsinterviewService.DeleteItrsinterview(oldITRS);
        if (responseInterview == true)
            return Ok(request);
        return Ok();
    }

    [HttpPut("[action]/{interviewId:guid}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> UpdateStatusInterview(
        Guid interviewId,
        string? Candidate_Status,
        string? Company_Status
    )
    {
        if (Candidate_Status == null && Company_Status == null)
            return StatusCode(StatusCodes.Status400BadRequest);

        var interviewNewStatus = await _interviewService.GetInterviewById(interviewId);

        if (interviewNewStatus == null)
        {
            return BadRequest();
        }

        var response = await _interviewService.UpdateStatusInterview(interviewId, Candidate_Status, Company_Status);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> DeleteInterview(Guid id)
    {
        return await _interviewService.DeleteInterview(id) ? Ok(true) : StatusCode(StatusCodes.Status404NotFound); ;
    }
}