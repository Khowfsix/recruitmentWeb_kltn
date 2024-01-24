using InsternShip.Data.ViewModels.Interviewer;

namespace InsternShip.Service.Interfaces;

public interface IInterviewerService
{
    Task<IEnumerable<InterviewerViewModel>> GetAllInterviewer();

    Task<InterviewerViewModel?> GetInterviewerById(Guid id);

    Task<IEnumerable<InterviewerViewModel?>> getInterviewersInDepartment(Guid departmentId);

    Task<InterviewerViewModel> SaveInterviewer(InterviewerAddModel addModel);

    Task<bool> UpdateInterviewer(InterviewerUpdateModel interviewerModel, Guid interviewerModelId);

    Task<bool> DeleteInterviewer(Guid interviewerModelId);
}