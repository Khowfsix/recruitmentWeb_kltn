using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.QuestionSkill;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class QuestionSkillService : IQuestionSkillService
    {
        private readonly IQuestionSkillRepository _questionSkillRepository;
        private readonly IMapper _mapper;

        public QuestionSkillService(IQuestionSkillRepository questionSkillRepository, IMapper mapper)
        {
            _questionSkillRepository = questionSkillRepository;
            _mapper = mapper;
        }

        public async Task<QuestionSkillViewModel> AddQuestionSkill(QuestionSkillAddModel questionSkill)
        {
            var data = _mapper.Map<QuestionSkillModel>(questionSkill);
            var response = await _questionSkillRepository.AddQuestionSkill(data);
            return _mapper.Map<QuestionSkillViewModel>(response);
        }

        public async Task<List<QuestionSkillViewModel>> GetAllQuestionSkills()
        {
            var modelDatas = await _questionSkillRepository.GetAllQuestionSkills();
            List<QuestionSkillViewModel> list = new List<QuestionSkillViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<QuestionSkillViewModel>(item));
            }
            return list;
        }

        public async Task<bool> RemoveQuestionSkill(Guid id)
        {
            return await _questionSkillRepository.RemoveQuestionSkill(id);
        }

        public async Task<bool> UpdateQuestionSkill(QuestionSkillUpdateModel questionSkill, Guid id)
        {
            var data = _mapper.Map<QuestionSkillModel>(questionSkill);
            return await _questionSkillRepository.UpdateQuestionSkill(data, id);
        }
    }
}