using InsternShip.Data.Models;

namespace InsternShip.Service.Interfaces{
    public interface IApplicationSuggestionService{
        Task<List<ApplicationModel>> GetSuggestion(Guid positionId);       
    }
}