using InsternShip.Data.Entities;
using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces;

public interface IInterviewRepository : IRepository<Interview>
{
    Task<IEnumerable<InterviewModel>> GetAllInterview();

    Task<InterviewModel?> GetInterviewById(Guid id);

    Task<InterviewModel?> GetInterviewById_NoInclude(Guid id);

    Task<IEnumerable<InterviewModel>> GetInterviewOfInterviewer(Guid id);

    Task<InterviewModel?> SaveInterview(InterviewModel request);

    //Task<bool> CreateInterviewWithApplication(InterviewModel interviewRequest, Guid applicationRequest);
    Task<bool> UpdateInterview(InterviewModel request, Guid requestId);

    Task<bool> DeleteInterview(Guid requestId);

    Task<IEnumerable<Interview>> InterviewReport(DateTime fromDate, DateTime toDate);
}