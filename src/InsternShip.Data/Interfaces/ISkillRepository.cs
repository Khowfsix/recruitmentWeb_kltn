using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<SkillModel>> GetAllSkills(string? request);
        Task<SkillModel> SaveSkill(SkillModel request);
        Task<bool> UpdateSkill(SkillModel request, Guid requestId);
        Task<bool> DeleteSkill(Guid requestId);
    }
}
