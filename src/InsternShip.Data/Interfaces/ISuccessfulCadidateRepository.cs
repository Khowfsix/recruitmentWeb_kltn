using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SuccessfulCadidate;
using System.Data.SqlTypes;

namespace InsternShip.Data.Interfaces
{
    public interface ISuccessfulCadidateRepository : IRepository<SuccessfulCadidate>
    {
        Task<IEnumerable<SuccessfulCadidateModel>> GetAllSuccessfulCadidates(string? request);
        Task<SuccessfulCadidateModel> SaveSuccessfulCadidate(SuccessfulCadidateModel request);
        Task<bool> UpdateSuccessfulCadidate(SuccessfulCadidateModel request, Guid requestId);
        Task<bool> DeleteSuccessfulCadidate(Guid requestId);
    }
}
