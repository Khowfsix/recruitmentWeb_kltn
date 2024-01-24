using InsternShip.Data.Entities;
using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<EventModel>> GetAllEvent();

    Task<EventModel> GetEventById(Guid id);

    Task<EventModel> SaveEvent(EventModel request);

    Task<bool> UpdateEvent(EventModel request, Guid requestId);

    Task<bool> DeleteEvent(Guid requestId);
}