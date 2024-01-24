using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SecurityAnswer;

namespace InsternShip.Service.Interfaces
{
    public interface ISecurityAnswerService
    {
        Task<IEnumerable<SecurityAnswerViewModel>> GetAllSecurityAnswers();
        Task<SecurityAnswerViewModel> SaveSecurityAnswer(SecurityAnswerAddModel viewModel);
        Task<bool> UpdateSecurityAnswer(SecurityAnswerUpdateModel reportModel, Guid reportModelId);
        Task<bool> DeleteSecurityAnswer(Guid reportModelId);
    }
}