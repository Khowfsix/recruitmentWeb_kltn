using InsternShip.Data.ViewModels.SecurityQuestion;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class SecurityQuestionController : BaseAPIController
    {
        private readonly ISecurityQuestionService _securityQuestionService;

        public SecurityQuestionController(ISecurityQuestionService securityQuestionService)
        {
            _securityQuestionService = securityQuestionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSecurityQuestion()
        {
            var listSecurityQuestion = await _securityQuestionService.GetAllSecurityQuestion();
            if (listSecurityQuestion == null)
            {
                return Ok("Not found");
            }
            return Ok(listSecurityQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSecurityQuestion(SecurityQuestionAddModel request)
        {
            if (request == null)
            {
                return Ok("Not found");
            }
            var listSecurityQuestion = await _securityQuestionService.SaveSecurityQuestion(request);
            return Ok(listSecurityQuestion);
        }

        [HttpPut("{categoryQuestionId:guid}")]
        public async Task<IActionResult> UpdateSecurityQuestion(SecurityQuestionUpdateModel request, Guid requestId)
        {
            if (request == null)
            {
                return Ok("Not found");
            }
            var listSecurityQuestion = await _securityQuestionService.UpdateSecurityQuestion(request, requestId);
            return Ok(listSecurityQuestion);
        }

        [HttpDelete("{requestId:guid}")]
        public async Task<IActionResult> DeleteSecurityQuestion(Guid requestId)
        {
            if (requestId == null)
            {
                return Ok("Not found");
            }
            var listCategoryQuestion = await _securityQuestionService.DeleteSecurityQuestion(requestId);
            return Ok(listCategoryQuestion);
        }
    }
}