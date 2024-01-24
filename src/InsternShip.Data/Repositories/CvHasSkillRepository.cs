using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CvHasSkillRepository : Repository<CvHasSkill>, ICvHasSkillrepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CvHasSkillRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCvHasSkillService(Guid requestId)
        {
            try
            {
                var cvHasSkill = GetById(requestId);
                if (cvHasSkill == null)
                    throw new ArgumentNullException(nameof(cvHasSkill));
                Entities.Remove(cvHasSkill);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CvHasSkillModel>> GetAllCvHasSkillService(string? request)
        {
            try
            {
                var listData = new List<CvHasSkillModel>();
                if (string.IsNullOrEmpty(request))
                {
                    var data = await Entities.ToListAsync();
                    foreach (var item in data)
                    {
                        var obj = _mapper.Map<CvHasSkillModel>(item);
                        listData.Add(obj);
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<CvModel>> GetCv(Guid skillId)
        {
            var data = await Entities
                .Where(x => x.SkillId == skillId)
                .Include(x => x.Cv)
                .Select(x => x.Cv)
                .ToListAsync();

            var resp = _mapper.Map<List<CvModel>>(data);

            return resp;
        }

        public async Task<IList<SkillModel>> GetSkill(Guid Cvid)
        {
            var data = await Entities
                .Where(x => x.Cvid == Cvid)
                .Include(x => x.Skill)
                .Select(x => x.Skill)
                .ToListAsync();

            var resp = _mapper.Map<List<SkillModel>>(data);

            return resp;
        }

        public async Task<CvHasSkillModel> SaveCvHasSkillService(CvHasSkillModel request)
        {
            try
            {
                var cvHasSkill = _mapper.Map<CvHasSkill>(request);
                cvHasSkill.CvSkillsId = Guid.NewGuid();

                Entities.Add(cvHasSkill);
                _uow.SaveChanges();

                var response = _mapper.Map<CvHasSkillModel>(cvHasSkill);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCvHasSkillService(CvHasSkillModel request, Guid requestId)
        {
            try
            {
                var cvHasSkill = _mapper.Map<CvHasSkill>(request);
                cvHasSkill.CvSkillsId = requestId;
                Entities.Update(cvHasSkill);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CvHasSkillModel>> GetAllSkillsFromOneCV(Guid Cvid)
        {
            var cvHasSkillList = Entities.AsAsyncEnumerable();
            List<CvHasSkillModel> result = new();
            await foreach (var skill in cvHasSkillList)
            {
                if (skill.Cvid == Cvid)
                {
                    result.Add(_mapper.Map<CvHasSkillModel>(skill));
                }
            }
            return result;
        }
    }
}