using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Requirement;

namespace InsternShip.Data.Interfaces
{
    public interface IRequirementRepository : IRepository<Requirement>
    {
        Task<IEnumerable<RequirementModel>> GetAllRequirement();
        Task<RequirementModel> SaveRequirement(RequirementModel request);
        Task<bool> UpdateRequirement(RequirementModel request, Guid requestId);
        Task<bool> DeleteRequirement(Guid requestId);
        Task<List<RequirementModel>> GetRequirementsByPositionId(Guid positionId);
    }
}
