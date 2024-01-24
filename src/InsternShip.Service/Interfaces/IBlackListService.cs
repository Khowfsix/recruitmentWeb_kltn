using InsternShip.Data.ViewModels.BlackList;

namespace InsternShip.Service.Interfaces
{
    public interface IBlacklistService
    {
        Task<IEnumerable<BlacklistViewModel>> GetAllBlackLists();

        Task<BlacklistViewModel> SaveBlackList(BlackListAddModel request);

        Task<bool> UpdateBlackList(BlackListUpdateModel request, Guid requestId);

        Task<bool> DeleteBlackList(Guid requestId);
    }
}