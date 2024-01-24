using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Skill;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillViewModel>> GetAllSkills(string? request)
        {
            var modelDatas = await _skillRepository.GetAllSkills(request);
            if (modelDatas != null)
            {
                List<SkillViewModel> list = new List<SkillViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<SkillViewModel>(item));
                }
                return list;
            }
            return null!;
        }

        public async Task<SkillViewModel> SaveSkill(SkillAddModel request)
        {
            var data = _mapper.Map<SkillModel>(request);
            var response = await _skillRepository.SaveSkill(data);
            return _mapper.Map<SkillViewModel>(response);
        }

        public async Task<bool> UpdateSkill(SkillUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<SkillModel>(request);
            return await _skillRepository.UpdateSkill(data, requestId);
        }

        public async Task<bool> DeleteSkill(Guid requestId)
        {
            return await _skillRepository.DeleteSkill(requestId);
        }
    }
}