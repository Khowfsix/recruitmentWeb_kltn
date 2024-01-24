using InsternShip.Data.Model;
using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<CandidateModel>> GetAllCandidates();

        Task<CandidateModel> SaveCandidate(CandidateModel request);

        Task<bool> UpdateCandidate(CandidateModel request, Guid requestId);

        Task<bool> DeleteCandidate(Guid requestId);

        Task<CandidateModel?> GetCandidateByUserId(string userId);

        Task<ProfileModel?> GetProfile(Guid candidateId);

        Task<CandidateModel> FindById(Guid id);
    }
}