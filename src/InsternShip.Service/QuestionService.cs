using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Question;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryQuestionRepository _categoryQuestionRepository;

        public QuestionService(IQuestionRepository questionRepository,
            IMapper mapper,
            ICategoryQuestionRepository categoryQuestionRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _categoryQuestionRepository = categoryQuestionRepository;
        }

        public async Task<QuestionViewModel> AddQuestion(QuestionAddModel entity)
        {
            var data = _mapper.Map<QuestionModel>(entity);
            var response = await _questionRepository.AddQuestion(data);
            return _mapper.Map<QuestionViewModel>(response);
        }

        public async IAsyncEnumerable<Task> AddQuestion(IAsyncEnumerable<QuestionAddModel> entities)
        {
            await foreach (QuestionAddModel obj in entities)
            {
                var data = _mapper.Map<QuestionModel>(obj);
                yield return _questionRepository.AddQuestion(data);
            }
        }

        public async Task<List<QuestionViewModel>> GetAllLanguageQuestions()
        {
            try
            {
                string cateQuestion = "Language";
                Guid id = await _categoryQuestionRepository.GetIdCategoryQuestion(cateQuestion);
                var modelDatas = await _questionRepository.GetListQuestions(id);
                List<QuestionViewModel> list = new List<QuestionViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<QuestionViewModel>(item));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<QuestionViewModel>> GetAllSoftSkillQuestions()
        {
            try
            {
                String cateQuestion = "SoftSkill";
                Guid id = await _categoryQuestionRepository.GetIdCategoryQuestion(cateQuestion);
                var modelDatas = await _questionRepository.GetListQuestions(id);
                List<QuestionViewModel> list = new List<QuestionViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<QuestionViewModel>(item));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<QuestionViewModel>> GetAllTechnologyQuestions()
        {
            try
            {
                String cateQuestion = "Technology";
                Guid id = await _categoryQuestionRepository.GetIdCategoryQuestion(cateQuestion);
                var modelDatas = await _questionRepository.GetListQuestions(id);
                List<QuestionViewModel> list = new List<QuestionViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<QuestionViewModel>(item));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IQuestionRepository Get_questionRepository()
        {
            return _questionRepository;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestions(string? query, Guid? questionId)
        {
            var list = new List<QuestionViewModel>();
            if (questionId != null)
            {
                var quest = await _questionRepository.GetQuestion(questionId);
                var response = _mapper.Map<QuestionViewModel>(quest);
                //var list = new List<QuestionViewModel>();
                list.Add(response);
                return list;
            }
            else if (query != null)
            {
                var modelDatas = new List<QuestionModel>();
                var quest = await _questionRepository.GetQuestionsByName(query);
                if (query != null || questionId != null)
                {
                    //List<QuestionViewModel> list = new List<QuestionViewModel>();
                    foreach (var item in modelDatas)
                    {
                        list.Add(_mapper.Map<QuestionViewModel>(item));
                    }
                    return list;
                }
            }
            else
            {
                var modelDatas = await _questionRepository.GetAllQuestions();
                //List<QuestionViewModel> list = new List<QuestionViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<QuestionViewModel>(item));
                }
                return list;
            }
            return list;
        }

        public async Task<QuestionViewModel> GetQuestion(Guid id)
        {
            var data = await _questionRepository.GetQuestion(id);
            return _mapper.Map<QuestionViewModel>(data);
        }

        public async Task<bool> RemoveQuestion(Guid id)
        {
            return await _questionRepository.RemoveQuestion(id);
        }

        public async Task<bool> UpdateQuestion(QuestionUpdateModel entity, Guid id)
        {
            var data = _mapper.Map<QuestionModel>(entity);
            return await _questionRepository.UpdateQuestion(data, id);
        }
    }
}