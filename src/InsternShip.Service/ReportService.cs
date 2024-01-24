using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Report;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class ReportService : IReportService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IMapper _mapper;

        public ReportService(IApplicationRepository applicationRepository, IReportRepository reportRepository, IMapper mapper, IInterviewRepository interviewRepository)
        {
            _applicationRepository = applicationRepository;
            _reportRepository = reportRepository;
            _mapper = mapper;
            _interviewRepository = interviewRepository;
        }

        public async Task<ReportViewModel> SaveReport(ReportAddModel reportModel)
        {
            var data = _mapper.Map<ReportModel>(reportModel);
            var response = await _reportRepository.SaveReport(data);
            return _mapper.Map<ReportViewModel>(response);
        }

        public async Task<bool> DeleteReport(Guid reportModelId)
        {
            return await _reportRepository.DeleteReport(reportModelId);
        }

        public async Task<IEnumerable<ReportViewModel>> GetAllReport()
        {
            var modelDatas = await _reportRepository.GetAllReport();
            List<ReportViewModel> list = new List<ReportViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<ReportViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateReport(ReportUpdateModel reportModel, Guid reportModelId)
        {
            var data = _mapper.Map<ReportModel>(reportModel);
            return await _reportRepository.UpdateReport(data, reportModelId);
        }

        public async Task<IEnumerable<InterviewReportViewModel>> InterviewReport(DateTime fromDate, DateTime toDate)
        {
            var reportData = await _interviewRepository.InterviewReport(fromDate, toDate);

            var result = new List<InterviewReportViewModel>();

            foreach (var item in reportData)
            {
                var row = new InterviewReportViewModel()
                {
                    InterviewId = item.InterviewId,
                    CandidateId = item.Application.Cv.CandidateId,
                    InterviewerId = item.InterviewerId,
                    ApplyDate = item.Application.DateTime,
                    Status = item.Company_Status!,
                    Score = item.Rounds.Average(x => x.Score) ?? 0,
                };

                result.Add(row);
            }

            return result;
        }

        public async Task<IEnumerable<ApplicationReportViewModel>> ApplicationReport(DateTime fromDate, DateTime toDate)
        {
            var reportData = await _applicationRepository.ApplicationReport(fromDate, toDate);

            var result = new List<ApplicationReportViewModel>();

            foreach (var item in reportData)
            {
                var row = new ApplicationReportViewModel()
                {
                    ApplicationId = item.ApplicationId,
                    FullName = item.Cv.Candidate.User.FullName,
                    DateOfBirth = item.Cv.Candidate.User.DateOfBirth,
                    Address = item.Cv.Candidate.User.Address,
                    Experience = item.Cv.Experience,
                    CvName = item.Cv.CvName,
                    Introduction = item.Cv.Introduction,
                    Education = item.Cv.Education,
                    PositionName = item.Position.PositionName,
                    Description = item.Position.Description,
                    Salary = item.Position.Salary,
                    DepartmentName = item.Position.Department.DepartmentName,
                    LanguageName = item.Position.Language.LanguageName,
                    DateTime = item.DateTime,
                    Candidate_Status = item.Candidate_Status ?? "Pending",
                    Company_Status = item.Company_Status ?? "Pending",
                    Priority = item.Priority,
                    IsDeleted = item.IsDeleted,
                };

                result.Add(row);
            }

            return result;
        }
    }
}