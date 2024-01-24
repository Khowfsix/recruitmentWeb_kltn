using InsternShip.Data.ViewModels.SecurityQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Service.Interfaces
{
    public interface ISecurityQuestionService
    {
        Task<IEnumerable<SecurityQuestionViewModel>> GetAllSecurityQuestion();
        Task<SecurityQuestionViewModel> SaveSecurityQuestion(SecurityQuestionAddModel request);
        Task<bool> UpdateSecurityQuestion(SecurityQuestionUpdateModel request, Guid requestId);
        Task<bool> DeleteSecurityQuestion(Guid requestId);
    }
}
