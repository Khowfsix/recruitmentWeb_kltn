using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Result;

namespace InsternShip.Service.Interfaces
{
    public interface IResultService
    {
        Task<IEnumerable<ResultViewModel>> GetAllResult();
        Task<ResultViewModel> SaveResult(ResultAddModel viewModel);
        Task<bool> UpdateResult(ResultUpdateModel reportModel, Guid reportModelId);
        Task<bool> DeleteResult(Guid reportModelId);
    }
}
