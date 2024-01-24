using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Recruiter;

namespace InsternShip.Data.Interfaces;

public interface IRecruiterRepository : IRepository<Recruiter>
{
    Task<IEnumerable<RecruiterModel>> GetAllRecruiter();
    Task<RecruiterModel?> GetRecruiterById(Guid id);
    Task<RecruiterModel?> SaveRecruiter(RecruiterModel request);
    Task<bool> UpdateRecruiter(RecruiterModel request, Guid requestId);
    Task<bool> DeleteRecruiter(Guid requestId);
}