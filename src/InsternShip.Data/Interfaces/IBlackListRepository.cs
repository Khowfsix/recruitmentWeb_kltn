using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces;

public interface IBlacklistRepository
{
    Task<IEnumerable<BlacklistModel>> GetAllBlackLists();

    Task<BlacklistModel> SaveBlackList(BlacklistModel request);

    Task<bool> UpdateBlackList(BlacklistModel request, Guid requestId);

    Task<bool> DeleteBlackList(Guid requestId);

    Task<bool> CheckIsInBlackList(Guid candidateId);
}