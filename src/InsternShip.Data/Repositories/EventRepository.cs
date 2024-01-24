using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EventRepository(RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventModel>> GetAllEvent()
    {
        var listData = new List<EventModel>();

        var data = await Entities.ToListAsync();
        foreach (var item in data)
        {
            //if (item.IsDeleted) continue;
            var obj = _mapper.Map<EventModel>(item);
            listData.Add(obj);
        }
        return listData;
    }

    public async Task<EventModel> GetEventById(Guid id)
    {
        var item = await Entities.FindAsync(id);
        if (item is null or { IsDeleted: true }) return null;
        var data = _mapper.Map<EventModel>(item);
        return data;
    }

    public async Task<EventModel> SaveEvent(EventModel request)
    {
        var obj = _mapper.Map<Event>(request);
        obj.EventId = Guid.NewGuid();
        Entities.Add(obj);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            //return await Task.FromResult(false);
            return null!;
        }

        var response = _mapper.Map<EventModel>(obj);
        return await Task.FromResult(response);
    }

    public async Task<bool> UpdateEvent(EventModel request, Guid requestId)
    {
        var entity = await Entities.AsNoTracking().FirstOrDefaultAsync(x => x.EventId == requestId);
        if (entity is null or { IsDeleted: true })
        {
            return await Task.FromResult(false);
        }
        var obj = _mapper.Map<Event>(request);
        obj.EventId = requestId;
        Entities.Update(obj);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteEvent(Guid requestId)
    {
        var entity = await Entities.FirstOrDefaultAsync(x => x.EventId == requestId);
        if (entity is null or { IsDeleted: true })
        {
            return await Task.FromResult(false);
        }

        entity.IsDeleted = true;
        Entities.Update(entity);
        _uow.SaveChanges();
        //try
        //{
        //    Entities.Remove(entity);
        //    _uow.SaveChanges();
        //}
        //catch (Exception e)
        //{
        //    // TODO: Return an object with error message
        //    Console.WriteLine(e.Message);
        //}
        return await Task.FromResult(true);
    }
}