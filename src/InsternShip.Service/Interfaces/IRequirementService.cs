using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Requirement;

namespace InsternShip.Service.Interfaces
{
    public interface IRequirementService
    {
        Task<IEnumerable<RequirementViewModel>> GetAllRequirement();
        Task<RequirementViewModel> SaveRequirement(RequirementAddModel viewModel);
        Task<bool> UpdateRequirement(RequirementUpdateModel reportModel, Guid reportModelId);
        Task<bool> DeleteRequirement(Guid reportModelId);
    }
}
