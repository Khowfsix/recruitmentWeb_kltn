using InsternShip.Data.ViewModels.Room;

namespace InsternShip.Service.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomViewModel>> GetAllRoom();

        Task<RoomViewModel> SaveRoom(RoomAddModel viewModel);

        Task<bool> UpdateRoom(RoomUpdateModel reportModel, Guid reportModelId);

        Task<bool> DeleteRoom(Guid reportModelId);
    }
}