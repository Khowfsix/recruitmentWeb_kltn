using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SecurityAnswer;

namespace InsternShip.Data.Interfaces
{
    public interface ISecurityAnswerRepository : IRepository<SecurityAnswer>
    {
        Task<IEnumerable<SecurityAnswerModel>> GetAllSecurityAnswers();
        Task<SecurityAnswerModel> SaveSecurityAnswer(SecurityAnswerModel request);
        Task<bool> UpdateSecurityAnswer(SecurityAnswerModel request, Guid requestId);
        Task<bool> DeleteSecurityAnswer(Guid requestId);
    }
}

