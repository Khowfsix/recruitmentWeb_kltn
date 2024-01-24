using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories;

public class InterviewRepository : Repository<Interview>, IInterviewRepository
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InterviewRepository(RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InterviewModel>> GetAllInterview()
    {
        var listData = new List<InterviewModel>();

        var data = await Entities
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t.Room)
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t.Shift)
            .Include(i => i.Recruiter.User)
            .Include(i => i.Interviewer.User)
            .Include(i => i.Application)
                .ThenInclude(a => a.Position.Department)
            .Include(i => i.Application)
                .ThenInclude(a => a.Cv)
                    .ThenInclude(c => c.Candidate.User)
            .Include(i => i.Rounds)
                .ThenInclude(r => r.Question)
            .ToListAsync();
        foreach (var item in data)
        {
            //if (item.IsDeleted) continue;
            var obj = _mapper.Map<InterviewModel>(item);
            listData.Add(obj);
        }

        return listData;
    }

    public async Task<InterviewModel?> GetInterviewById(Guid id)
    {
        var item = await Entities
            .Where(i => i.InterviewId.Equals(id))
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t!.Room)
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t!.Shift)
            .Include(i => i.Recruiter.User)
            .Include(i => i.Interviewer.User)
            .Include(i => i.Application)
                .ThenInclude(a => a.Position.Department)
            .Include(i => i.Application)
                .ThenInclude(a => a.Cv)
                    .ThenInclude(c => c.Candidate.User)
            .Include(i => i.Rounds)
                .ThenInclude(r => r.Question)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (item is null)
            return null;

        var result = _mapper.Map<InterviewModel>(item);
        return result;
    }

    public async Task<InterviewModel?> GetInterviewById_NoInclude(Guid id)
    {
        var item = await Entities
            .Where(i => i.InterviewId.Equals(id))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (item is null)
            return null;

        var result = _mapper.Map<InterviewModel>(item);
        return result;
    }

    public async Task<InterviewModel?> SaveInterview(InterviewModel request)
    {
        var interview = _mapper.Map<Interview>(request);
        interview.InterviewId = Guid.NewGuid();

        Entities.Add(interview);
        try { _uow.SaveChanges(); }
        catch (Exception e)
        {
            // TODO: Return an object with error message
            Console.WriteLine(e.Message);
            return null!;
        }

        var response = _mapper.Map<InterviewModel>(interview);
        return await Task.FromResult(response);
    }

    public async Task<bool> UpdateInterview(InterviewModel request, Guid requestId)
    {
        try
        {
            var interview = _mapper.Map<Interview>(request);
            interview.InterviewId = requestId;

            Entities.Update(interview);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> DeleteInterview(Guid requestId)
    {
        var entity = await Entities.FirstOrDefaultAsync(x => x.InterviewId == requestId);
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

    public async Task<IEnumerable<InterviewModel>> GetInterviewOfInterviewer(Guid id)
    {
        var listData = new List<InterviewModel>();

        var data = await Entities.Where(i => i.InterviewerId.Equals(id))
            .Where(i => i.InterviewId.Equals(id))
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t!.Room)
            .Include(i => i.Itrsinterview)
                .ThenInclude(t => t!.Shift)
            .Include(i => i.Recruiter.User)
            .Include(i => i.Interviewer.User)
            .Include(i => i.Application)
                .ThenInclude(a => a.Position.Department)
            .Include(i => i.Application)
                .ThenInclude(a => a.Cv)
                    .ThenInclude(c => c.Candidate.User)
            .Include(i => i.Rounds)
                .ThenInclude(r => r.Question)
            .AsNoTracking()
            .ToListAsync();
        foreach (var item in data)
        {
            if (item.IsDeleted) continue;
            var obj = _mapper.Map<InterviewModel>(item);
            listData.Add(obj);
        }

        return listData;
    }

    public async Task<IEnumerable<Interview>> InterviewReport(DateTime fromDate, DateTime toDate)
    {
        var interview = await Entities
            .Include(x => x.Application)
                .ThenInclude(x => x.Cv)
                .ThenInclude(x => x.Candidate)
            .Include(x => x.Recruiter)
            .Include(x => x.Interviewer)
            .Include(x => x.Result)
            .Include(x => x.Rounds)
            .Where(x => fromDate <= x.Application.DateTime && x.Application.DateTime <= toDate)
            .ToListAsync();

        return interview;
    }
}