using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICategoryQuestionRepository
    {
        Task<IEnumerable<CategoryQuestionModel>> GetAllCategoryQuestions();

        Task<CategoryQuestionModel?> GetCategoryQuestionById(Guid id);

        Task<IEnumerable<CategoryQuestionModel>> GetCategoryQuestionsByName(string keyword);

        Task<IEnumerable<CategoryQuestionModel>> GetCategoryQuestionsByWeight(double weight);

        Task<CategoryQuestionModel> SaveCategoryQuestion(CategoryQuestionModel categoryQuestion);

        Task<bool> UpdateCategoryQuestion(CategoryQuestionModel categoryQuestion, Guid categoryQuestionId);

        Task<bool> DeleteCategoryQuestion(Guid requestId);

        Task<Guid> GetIdCategoryQuestion(string keyword);
    }
}