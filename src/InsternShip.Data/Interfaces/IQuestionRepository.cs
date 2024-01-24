using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Question;

namespace InsternShip.Data.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<QuestionModel>> GetAllQuestions();

        Task<QuestionModel> GetQuestion(Guid? id);
        Task<List<QuestionModel>> GetListQuestions(Guid id);

        //Task<List<CategoryQuestionModel>> GetAllQuestionCategories();
        Task<QuestionModel> AddQuestion(QuestionModel question);

        Task<bool> UpdateQuestion(QuestionModel question, Guid id);

        Task<bool> RemoveQuestion(Guid id);
        Task<List<QuestionModel>> GetQuestionsByName(string keyword);
    }
}