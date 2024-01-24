using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories;

public class RecruiterRepository : Repository<Recruiter>, IRecruiterRepository
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RecruiterRepository(RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecruiterModel>> GetAllRecruiter()
    {
        var listData = new List<RecruiterModel>();

        var data = await Entities.Include(x => x.User).ToListAsync();
        foreach (var item in data)
        {
            //if (item.IsDeleted) continue;
            RecruiterModel obj = _mapper.Map<RecruiterModel>(item);
            listData.Add(obj);
        }

        return listData;
    }

    public async Task<RecruiterModel?> GetRecruiterById(Guid id)
    {
        var item = await Entities.Include(x => x.User).Where(r => r.RecruiterId == id).FirstOrDefaultAsync();
        if (item is null) return null;
        return _mapper.Map<RecruiterModel>(item);
    }

    public async Task<RecruiterModel?> SaveRecruiter(RecruiterModel request)
    {
        Recruiter obj = _mapper.Map<Recruiter>(request);
        obj.RecruiterId = Guid.NewGuid();

        Entities.Add(obj);
        _uow.SaveChanges();
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            return null!;
        }

        var response = _mapper.Map<RecruiterModel>(obj);
        return await Task.FromResult(response);
    }

    public async Task<bool> UpdateRecruiter(RecruiterModel request, Guid requestId)
    {
        Recruiter obj = _mapper.Map<Recruiter>(request);
        obj.RecruiterId = requestId;

        Entities.Update(obj);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteRecruiter(Guid requestId)
    {
        var entity = await Entities.FirstOrDefaultAsync(x => x.RecruiterId == requestId);
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