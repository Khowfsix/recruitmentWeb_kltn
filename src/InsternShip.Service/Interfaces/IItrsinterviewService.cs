using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Itrsinterview;

namespace InsternShip.Service.Interfaces;

public interface IItrsinterviewService
{
    Task<IEnumerable<ItrsinterviewViewModel>> GetAllItrsinterview();
    Task<ItrsinterviewViewModel?> GetItrsinterviewById(Guid id);
    Task<ItrsinterviewViewModel?> SaveItrsinterview(ItrsinterviewAddModel viewModel, Guid interviewerId);
    Task<bool> UpdateItrsinterview(ItrsinterviewUpdateModel itrsinterviewModel, Guid itrsinterviewModelId, Guid interviewerId);
    Task<bool> DeleteItrsinterview(Guid itrsinterviewModelId);
}