using InsternShip.Data.ViewModels.CandidateJoinEvent;

namespace InsternShip.Service.Interfaces
{
    public interface ICandidateJoinEventService
    {
        Task<IEnumerable<CandidateJoinEventViewModel>> GetAllCandidateJoinEvents();

        Task<CandidateJoinEventViewModel> SaveCandidateJoinEvent(CandidateJoinEventAddModel request);

        Task<bool> UpdateCandidateJoinEvent(CandidateJoinEventUpdateModel request, Guid requestId);

        Task<bool> DeleteCandidateJoinEvent(Guid requestId);

        Task<IEnumerable<CandidateJoinedEvent>> JoinEventDetail(Guid id);

        Task<IEnumerable<CandidateJoinEventViewModel>> GetCandidatesSortedByJoinEventCount();
    }
}