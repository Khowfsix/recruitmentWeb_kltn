using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Event;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service;

public class EventService : IEventService
{
    private readonly IEventRepository _EventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _EventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventViewModel> SaveEvent(EventAddModel eventModel)
    {
        var data = _mapper.Map<EventModel>(eventModel);
        var response = await _EventRepository.SaveEvent(data);
        return _mapper.Map<EventViewModel>(response);
    }

    public async Task<bool> DeleteEvent(Guid eventModelId)
    {
        return await _EventRepository.DeleteEvent(eventModelId);
    }

    public async Task<IEnumerable<EventViewModel>> GetAllEvent()
    {
        var data = await _EventRepository.GetAllEvent();
        List<EventViewModel> result = new List<EventViewModel>();
        if (data != null)
        {
            foreach (var item in data)
            {
                var obj = _mapper.Map<EventViewModel>(item);
                result.Add(obj);
            }
            return result;
        }
        return null;
    }

    public async Task<bool> UpdateEvent(EventUpdateModel eventUpdateModel, Guid eventModelId)
    {
        var data = _mapper.Map<EventModel>(eventUpdateModel);
        return await _EventRepository.UpdateEvent(data, eventModelId);
    }

    public async Task<EventViewModel> GetEventById(Guid id)
    {
        var data = await _EventRepository.GetEventById(id);
        if (data == null)
        {
            return null;
        }
        var respone = _mapper.Map<EventViewModel>(data);
        return respone;
    }
}