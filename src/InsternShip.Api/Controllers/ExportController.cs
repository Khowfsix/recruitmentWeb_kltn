using InsternShip.Common.ExportExcel;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class ExportController : BaseAPIController
    {
        private readonly IReportService _reportService;
        private readonly IBlacklistService _blacklistService;
        private readonly ICertificateService _certificateService;
        private readonly ISkillService _skillService;
        private readonly UserManager<WebUser> _userManager;

        public ExportController(
            IReportService reportService,
            ICertificateService certificateService,
            IBlacklistService blacklistService,
            ISkillService skillService,
            UserManager<WebUser> userManager)
        {
            _reportService = reportService;
            _blacklistService = blacklistService;
            _certificateService = certificateService;
            _skillService = skillService;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExportBlacklist()
        {
            var data = await _blacklistService.GetAllBlackLists();

            string[] columns = { "BlacklistId", "CandidateId", "Reason", "DateTime", "Status", "IsDeleted" };
            byte[] filecontent = ExportExcelHelper.ExportExcel(data.ToList(), $"{nameof(BlackList)} Report", true, columns);
            return File(filecontent, ExportExcelHelper.ExcelContentType, $"{nameof(BlackList)}_{DateTime.Today.ToString()}.xlsx");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExportCertificate()
        {
            var data = await _certificateService.GetAllCertificate(null);

            string[] columns = { "CertificateId", "CertificateName", "Description", "OrganizationName", "DateEarned", "ExpirationDate", "Link", "Cvid", "IsDeleted" };
            byte[] filecontent = ExportExcelHelper.ExportExcel(data.ToList(), $"{nameof(Certificate)} Report", true, columns);
            return File(filecontent, ExportExcelHelper.ExcelContentType, $"{nameof(Certificate)}_{DateTime.Today.ToString()}.xlsx");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExportSkill()
        {
            var data = await _skillService.GetAllSkills(null);

            string[] columns = { "SkillId", "SkillName", "Description", "IsDeleted" };
            byte[] filecontent = ExportExcelHelper.ExportExcel(data.ToList(), $"{nameof(Skill)} Report", true, columns);
            return File(filecontent, ExportExcelHelper.ExcelContentType, $"{nameof(Skill)}_{DateTime.Today.ToString()}.xlsx");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExportApplication(DateTime fromDate, DateTime toDate)
        {
            var data = await _reportService.ApplicationReport(fromDate, toDate);

            string[] columns = {
                "ApplicationId", "FullName", "DateOfBirth", "Address", "Experience",
                "CVName", "Introduction", "Education", "PositionName", "Description",
                "Salary", "DepartmentName", "LanguageName", "DateTime", "CandidateStatus",
                "CompanyStatus", "Priority", "IsDeleted"
                               };
            byte[] filecontent = ExportExcelHelper.ExportExcel(data.ToList(), $"{nameof(Application)} Report", true, columns);
            return File(filecontent, ExportExcelHelper.ExcelContentType, $"{nameof(Application)}_{DateTime.Today.ToString()}.xlsx");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InterviewReport(DateTime fromDate, DateTime toDate)
        {
            var data = await _reportService.InterviewReport(fromDate, toDate);

            string[] columns = { "InterviewId", "CandidateId", "InterviewerId", "ApplyDate", "Status", "Score" };
            byte[] filecontent = ExportExcelHelper.ExportExcel(data.ToList(), $"{nameof(Interview)} Report", true, columns);
            return File(filecontent, ExportExcelHelper.ExcelContentType, $"{nameof(Interview)}_{DateTime.Today.ToString()}.xlsx");
        }
    }
}