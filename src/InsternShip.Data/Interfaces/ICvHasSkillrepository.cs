using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICvHasSkillrepository
    {
        Task<IEnumerable<CvHasSkillModel>> GetAllCvHasSkillService(string? request);

        Task<CvHasSkillModel> SaveCvHasSkillService(CvHasSkillModel request);

        Task<bool> UpdateCvHasSkillService(CvHasSkillModel request, Guid requestId);

        Task<bool> DeleteCvHasSkillService(Guid requestId);

        Task<IList<SkillModel>> GetSkill(Guid Cvid);

        Task<IList<CvModel>> GetCv(Guid skillId);

        Task<List<CvHasSkillModel>> GetAllSkillsFromOneCV(Guid Cvid);
    }
}