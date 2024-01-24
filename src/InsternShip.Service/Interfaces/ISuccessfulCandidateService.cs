using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SuccessfulCadidate;

namespace InsternShip.Service.Interfaces
{
    public interface ISuccessfulCandidateService
    {
        Task<IEnumerable<SuccessfulCadidateViewModel>> GetAllSuccessfulCadidates(string? request);

        Task<SuccessfulCadidateViewModel> SaveSuccessfulCadidate(SuccessfulCadidateAddModel request);

        Task<bool> UpdateSuccessfulCadidate(SuccessfulCadidateUpdateModel request, Guid requestId);

        Task<bool> DeleteSuccessfulCadidate(Guid requestId);
    }
}