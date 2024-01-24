using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Skill;

namespace InsternShip.Service.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillViewModel>> GetAllSkills(string? request);

        Task<SkillViewModel> SaveSkill(SkillAddModel request);

        Task<bool> UpdateSkill(SkillUpdateModel request, Guid requestId);

        Task<bool> DeleteSkill(Guid requestId);
    }
}