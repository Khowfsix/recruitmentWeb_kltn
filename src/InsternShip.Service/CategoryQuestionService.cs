using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.CategoryQuestion;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class CategoryQuestionService : ICategoryQuestionService
    {
        private readonly ICategoryQuestionRepository _categoryQuestionRepository;
        private readonly IMapper _mapper;

        public CategoryQuestionService(ICategoryQuestionRepository categoryQuestionRepository, IMapper mapper)
        {
            _categoryQuestionRepository = categoryQuestionRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCategoryQuestion(Guid requestId)
        {
            return await _categoryQuestionRepository.DeleteCategoryQuestion(requestId);
        }

        public async Task<IEnumerable<CategoryQuestionViewModel>> GetAllCategoryQuestions()
        {
            var data = await _categoryQuestionRepository.GetAllCategoryQuestions();
            return data.Select(item => _mapper.Map<CategoryQuestionViewModel>(item)).ToList();
        }

        public async Task<CategoryQuestionViewModel?> GetCategoryQuestionById(Guid id)
        {
            var data = await _categoryQuestionRepository.GetCategoryQuestionById(id);
            if (data == null) return null;
            var response = _mapper.Map<CategoryQuestionViewModel>(data);
            return response;
        }

        public async Task<IEnumerable<CategoryQuestionViewModel>> GetCategoryQuestionsByName(string keyword)
        {
            var data = await _categoryQuestionRepository.GetCategoryQuestionsByName(keyword);
            return data.Select(item => _mapper.Map<CategoryQuestionViewModel>(item)).ToList();
        }

        public async Task<IEnumerable<CategoryQuestionViewModel>> GetCategoryQuestionsByWeight(double weight)
        {
            var data = await _categoryQuestionRepository.GetCategoryQuestionsByWeight(weight);
            return data.Select(item => _mapper.Map<CategoryQuestionViewModel>(item)).ToList();
        }

        public async Task<CategoryQuestionViewModel> SaveCategoryQuestion(CategoryQuestionAddModel categoryQuestion)
        {
            var data = _mapper.Map<CategoryQuestionModel>(categoryQuestion);
            var response = await _categoryQuestionRepository.SaveCategoryQuestion(data);

            return _mapper.Map<CategoryQuestionViewModel>(response);
        }

        public async Task<bool> UpdateCategoryQuestion(CategoryQuestionUpdateModel categoryQuestion, Guid categoryQuestionId)
        {
            var data = _mapper.Map<CategoryQuestionModel>(categoryQuestion);
            return await _categoryQuestionRepository.UpdateCategoryQuestion(data, categoryQuestionId);
        }
    }
}