using InsternShip.Data.ViewModels.Round;

namespace InsternShip.Service.Interfaces
{
    public interface IRoundService
    {
        Task<IEnumerable<RoundViewModel>> GetAllRounds(string? interviewId);

        Task<IEnumerable<RoundViewModel>> GetRoundsOfInterview(Guid interviewId);

        Task<RoundViewModel> SaveRound(RoundAddModel roundModel);

        Task<bool> UpdateRound(RoundUpdateModel roundModel, Guid roundId);

        Task<bool> DeleteRound(Guid roundId);
    }
}