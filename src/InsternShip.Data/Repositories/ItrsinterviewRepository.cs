using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Itrsinterview;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace InsternShip.Data.Repositories;

public class ItrsinterviewRepository : Repository<Itrsinterview>, IItrsinterviewRepository
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ItrsinterviewRepository(RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItrsinterviewModel>> GetAllItrsinterview()
    {
        var listData = new List<ItrsinterviewModel>();

        var data = await Entities
            .Include(i => i.Room)
            .Include(i => i.Shift)
            .ToListAsync();
        foreach (var item in data)
        {
            var obj = _mapper.Map<ItrsinterviewModel>(item);
            listData.Add(obj);
        }

        return listData;
    }

    public async Task<IEnumerable<ItrsinterviewModel>> GetAllItrsinterview_NoInclude()
    {
        var listData = new List<ItrsinterviewModel>();

        var data = await Entities
            .AsNoTracking()
            .ToListAsync();
        foreach (var item in data)
        {
            var obj = _mapper.Map<ItrsinterviewModel>(item);
            listData.Add(obj);
        }

        return listData;
    }

    public async Task<ItrsinterviewModel?> GetItrsinterviewById(Guid id)
    {
        var item = await Entities
            .Include(i => i.Room)
            .Include(i => i.Shift)
            .Where(i => i.ItrsinterviewId.Equals(id))
            .FirstOrDefaultAsync();
        if (item is null) return null;
        var data = _mapper.Map<ItrsinterviewModel>(item);
        return data;
    }

    public async Task<ItrsinterviewModel?> SaveItrsinterview(ItrsinterviewModel request, Guid interviewerId)
    {
        var obj = _mapper.Map<Itrsinterview>(request);
        var newId = Guid.NewGuid();
        obj.ItrsinterviewId = newId;

        _uow.BeginTransaction();

        Entities.Add(obj);
        try
        {
            _uow.SaveChanges();
        }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            _uow.RollbackTransaction();
            _uow.Dispose();

            return null!;
        }
        _uow.CommitTransaction();

        var response = _mapper.Map<ItrsinterviewModel>(obj);
        return await Task.FromResult(response);
    }

    public async Task<bool> UpdateItrsinterview(ItrsinterviewModel request, Guid requestId)
    {
        var obj = _mapper.Map<Itrsinterview>(request);
        obj.ItrsinterviewId = requestId;
        Entities.Update(obj);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteItrsinterview(Guid requestId)
    {
        var entity = await Entities.FirstOrDefaultAsync(x => x.ItrsinterviewId == requestId);
        if (entity == null)
        {
            return await Task.FromResult(false);
        }

        try
        {
            Entities.Remove(entity);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
        //return await Task.FromResult(false);
    }
}