using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Interview;

namespace InsternShip.Service.Interfaces;

public interface IInterviewService
{
    Task<IEnumerable<InterviewViewModel>> GetAllInterview(string status);

    Task<IEnumerable<InterviewViewModel>> GetInterviewsByInterviewer(Guid requestId);

    Task<IEnumerable<InterviewViewModel>> GetInterviewsByPositon(Guid requestId);

    Task<IEnumerable<InterviewViewModel>> GetInterviewsByDepartment(Guid requestId);

    Task<InterviewViewModel?> GetInterviewById(Guid id);

    Task<InterviewViewModel> SaveInterview(InterviewAddModel viewModel);

    //Task<bool> CreateInterviewWithApplication(InterviewAddModel interviewRequest, Guid applicationId);
    Task<bool> UpdateInterview(InterviewUpdateModel interviewModel, Guid interviewModelId);

    Task<bool> UpdateStatusInterview(Guid interviewId, string? Candidate_Status, string? Company_Status);

    Task<bool> DeleteInterview(Guid interviewModelId);

    Task<InterviewModel?> PostQuestionIntoInterview(InterviewResultQuestionModel request);

    Task<InterviewModel?> GetInterviewById_noInclude(Guid id);
}