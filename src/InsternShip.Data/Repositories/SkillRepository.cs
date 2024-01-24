using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SkillRepository(RecruitmentWebContext dbContext,
            IUnitOfWork uow,
            IMapper mapper) : base(dbContext)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillModel>> GetAllSkills(string? request)
        {
            var listData = new List<SkillModel>();
            if (string.IsNullOrEmpty(request))
            {
                var datas = await Entities.Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<SkillModel>(data);
                    listData.Add(obj);
                }
                return listData;
            }
            else
            {
                var datas = await Entities.Where(s => s.SkillName.Contains(request)).Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<SkillModel>(data);
                    listData.Add(obj);
                }
                return listData;
            }
        }

        public async Task<SkillModel> SaveSkill(SkillModel request)
        {
            var skill = _mapper.Map<Skill>(request);
            skill.SkillId = Guid.NewGuid();

            var response = _mapper.Map<SkillModel>(skill);

            Entities.Add(skill);
            _uow.SaveChanges();
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateSkill(SkillModel request, Guid requestId)
        {
            var round = _mapper.Map<Skill>(request);
            round.SkillId = requestId;

            Entities.Update(round);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSkill(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.SkillId == requestId);
            if (entity is null or { IsDeleted: true })
            {
                return await Task.FromResult(false);
            }
            entity.IsDeleted = true;
            Entities.Update(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}