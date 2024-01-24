using InsternShip.Data.ViewModels.CategoryQuestion;

namespace InsternShip.Service.Interfaces
{
    public interface ICategoryQuestionService
    {
        Task<IEnumerable<CategoryQuestionViewModel>> GetAllCategoryQuestions();

        Task<CategoryQuestionViewModel?> GetCategoryQuestionById(Guid id);

        Task<IEnumerable<CategoryQuestionViewModel>> GetCategoryQuestionsByName(string keyword);

        Task<IEnumerable<CategoryQuestionViewModel>> GetCategoryQuestionsByWeight(double weight);

        Task<CategoryQuestionViewModel> SaveCategoryQuestion(CategoryQuestionAddModel categoryQuestion);

        Task<bool> UpdateCategoryQuestion(CategoryQuestionUpdateModel categoryQuestion, Guid categoryQuestionId);

        Task<bool> DeleteCategoryQuestion(Guid requestId);
    }
}