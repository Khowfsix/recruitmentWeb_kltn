using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Candidate;
using InsternShip.Data.ViewModels.Report;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize(Roles = "Recruiter,Interviewer,Admin")]
    public class ReportController : BaseAPIController
    {
        private readonly IReportService _reportService;
        private readonly ICandidateService _candidateService;
        private readonly IInterviewerService _interviewerService;
        private readonly UserManager<WebUser> _userManager;

        public ReportController(
            IReportService reportService,
            ICandidateService candidateService,
            IInterviewerService interviewerService,
            UserManager<WebUser> userManager)
        {
            _reportService = reportService;
            _candidateService = candidateService;
            _interviewerService = interviewerService;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InterviewReport(DateTime fromDate, DateTime toDate)
        {
            var reportList = await _reportService.InterviewReport(fromDate, toDate);

            foreach (var row in reportList)
            {
                var candidateId = row.CandidateId;
                var interviewerId = row.InterviewerId;

                var candidate = await _candidateService.FindById(candidateId);

                var interviewer = await _interviewerService.GetInterviewerById(interviewerId);

                if (candidate != null)
                {
                    var candidateProfile = await _userManager.FindByIdAsync(candidate.UserId);

                    if (candidateProfile == null)
                    {
                        return NotFound("User Not Found");
                    }

                    row.CandidateName = candidateProfile.FullName ?? "";
                }

                if (interviewer != null)
                {
                    var interviewerProfile = await _userManager.FindByIdAsync(interviewer.UserId);

                    if (interviewerProfile == null)
                    {
                        return NotFound("User Not Found");
                    }

                    row.InterviewerName = interviewerProfile.FullName ?? "";
                }
            }

            return Ok(reportList);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ApplicationReport(DateTime fromDate, DateTime toDate)
        {
            var reportList = await _reportService.ApplicationReport(fromDate, toDate);

            return Ok(reportList);
        }
    }
}