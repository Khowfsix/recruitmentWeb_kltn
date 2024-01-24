using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Result;

namespace InsternShip.Data.Interfaces
{
    public interface IResultRepository : IRepository<Result>
    {
        Task<IEnumerable<ResultModel>> GetAllResult();
        Task<ResultModel> SaveResult(ResultModel request);
        Task<bool> UpdateResult(ResultModel request, Guid requestId);
        Task<bool> DeleteResult(Guid requestId);
    }
}
