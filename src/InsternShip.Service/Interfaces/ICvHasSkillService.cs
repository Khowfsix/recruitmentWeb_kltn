using InsternShip.Data.ViewModels.CvHasSkill;

namespace InsternShip.Service.Interfaces
{
    public interface ICvHasSkillService
    {
        Task<IEnumerable<CvHasSkillViewModel>> GetAllCvHasSkillService(string? request);

        Task<CvHasSkillViewModel> SaveCvHasSkillService(CvHasSkillAddModel request);

        Task<bool> UpdateCvHasSkillService(CvHasSkillUpdateModel request, Guid requestId);

        Task<bool> DeleteCvHasSkillService(Guid requestId);
    }
}