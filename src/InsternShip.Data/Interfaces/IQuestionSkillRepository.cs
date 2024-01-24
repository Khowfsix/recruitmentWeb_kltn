using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.QuestionSkill;

namespace InsternShip.Data.Interfaces
{
    public interface IQuestionSkillRepository : IRepository<QuestionSkill>
    {
        Task<List<QuestionSkillModel>> GetAllQuestionSkills();

        Task<QuestionSkillModel> AddQuestionSkill(QuestionSkillModel questionSkill);

        Task<bool> UpdateQuestionSkill(QuestionSkillModel questionSkill, Guid id);

        Task<bool> RemoveQuestionSkill(Guid id);
    }
}