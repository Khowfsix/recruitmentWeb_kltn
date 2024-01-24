using InsternShip.Data.ViewModels.Application;

namespace InsternShip.Service.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationViewModel>> GetAllApplications();

        Task<IEnumerable<ApplicationViewModel>> GetAllApplicationsOfPosition(Guid? positionId, string? status, string? priority);

        Task<IEnumerable<ApplicationHistoryViewModel>> GetApplicationHistory(Guid candidateId);

        Task<ApplicationViewModel?> GetApplicationById(Guid ApplicationId);

        Task<IEnumerable<ApplicationViewModel>> GetApplicationsWithStatus(string status, string priority);

        Task<ApplicationViewModel> SaveApplication(ApplicationAddModel requestId);

        Task<bool> UpdateApplication(ApplicationUpdateModel request, Guid applicationId);

        Task<bool> UpdateStatusApplication(Guid applicationId, string? Candidate_Status, string? Company_Status);

        Task<bool> DeleteApplication(Guid applicationId);
    }
}