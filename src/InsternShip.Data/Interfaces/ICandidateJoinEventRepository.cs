using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICandidateJoinEventRepository
    {
        Task<IEnumerable<CandidateJoinEventModel>> GetAllCandidateJoinEvents();

        Task<CandidateJoinEventModel> SaveCandidateJoinEvent(CandidateJoinEventModel request);

        Task<bool> UpdateCandidateJoinEvent(CandidateJoinEventModel request, Guid requestId);

        Task<bool> DeleteCandidateJoinEvent(Guid requestId);

        Task<IEnumerable<CandidateJoinEventModel>> JoinEventDetail(Guid id);

        Task<IEnumerable<CandidateJoinEventModel>> GetCandidatesSortedByJoinEventCount();
    }
}