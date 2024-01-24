using InsternShip.Data.ViewModels.Event;

namespace InsternShip.Service.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventViewModel>> GetAllEvent();

    Task<EventViewModel> GetEventById(Guid id);

    Task<EventViewModel> SaveEvent(EventAddModel viewModel);

    Task<bool> UpdateEvent(EventUpdateModel eventModel, Guid eventModelId);

    Task<bool> DeleteEvent(Guid eventModelId);
}