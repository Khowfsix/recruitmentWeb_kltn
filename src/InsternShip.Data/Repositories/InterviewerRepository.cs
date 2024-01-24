using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories;

public class InterviewerRepository : Repository<Interviewer>, IInterviewerRepository
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InterviewerRepository(RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InterviewerModel>> GetAllInterviewer()
    {
        var listData = new List<InterviewerModel>();

        var data = await Entities.Include(x => x.User).ToListAsync();
        foreach (var item in data)
        {
            //if (item.IsDeleted) continue;
            var obj = _mapper.Map<InterviewerModel>(item);
            listData.Add(obj);
        }
        return listData;
    }

    public async Task<InterviewerModel?> GetInterviewerById(Guid id)
    {
        var item = await Entities.Include(x => x.User).Where(x => x.InterviewerId == id).FirstOrDefaultAsync();
        //if (item is null or { IsDeleted: true }) return null;
        var data = _mapper.Map<InterviewerModel>(item);
        return data;
    }

    public async Task<InterviewerModel> SaveInterviewer(InterviewerModel request)
    {
        var interviewer = _mapper.Map<Interviewer>(request);
        interviewer.InterviewerId = Guid.NewGuid();

        Entities.Add(interviewer);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            //return await Task.FromResult(false);
            return null!;
        }

        var response = _mapper.Map<InterviewerModel>(interviewer);
        return await Task.FromResult(response);
    }

    public async Task<bool> UpdateInterviewer(InterviewerModel request, Guid requestId)
    {
        var interviewer = _mapper.Map<Interviewer>(request);
        interviewer.InterviewerId = requestId;
        Entities.Update(interviewer);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteInterviewer(Guid requestId)
    {
        var entity = await Entities.FirstOrDefaultAsync(x => x.InterviewerId == requestId);
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

    public async Task<IEnumerable<InterviewerModel>> getInterviewersInDepartment(Guid deparmentId)
    {
        var listData = new List<InterviewerModel>();

        var data = await Entities.Include(x => x.User).Where(i => i.DepartmentId.Equals(deparmentId)).ToListAsync();
        foreach (var item in data)
        {
            //if (item.IsDeleted) continue;
            var obj = _mapper.Map<InterviewerModel>(item);
            listData.Add(obj);
        }
        return listData;
    }
}