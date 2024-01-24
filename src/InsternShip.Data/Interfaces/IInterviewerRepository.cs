using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Interviewer;

namespace InsternShip.Data.Interfaces;

public interface IInterviewerRepository : IRepository<Interviewer>
{
    Task<IEnumerable<InterviewerModel>> GetAllInterviewer();
    Task<InterviewerModel?> GetInterviewerById(Guid id);
    Task<IEnumerable<InterviewerModel>> getInterviewersInDepartment(Guid deparmentId);
    Task<InterviewerModel> SaveInterviewer(InterviewerModel request);
    Task<bool> UpdateInterviewer(InterviewerModel request, Guid requestId);
    Task<bool> DeleteInterviewer(Guid requestId);
}