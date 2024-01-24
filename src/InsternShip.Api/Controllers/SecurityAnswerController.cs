using InsternShip.Data.ViewModels.SecurityAnswer;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class SecurityAnswerController : BaseAPIController
    {
        private readonly ISecurityAnswerService _securityAnswerService;

        public SecurityAnswerController(ISecurityAnswerService securityAnswerService)
        {
            _securityAnswerService = securityAnswerService;
        }

        // GET: api/<SecurityQuestionController>
        [HttpGet]
        public async Task<IActionResult> GetAllSecurityAnswers(string req)
        {
            var reportList = await _securityAnswerService.GetAllSecurityAnswers();
            return Ok(reportList);
        }

        // POST api/<SecurityQuestionController>
        [HttpPost]
        public async Task<IActionResult> SaveSecurityAnswer(SecurityAnswerAddModel req)
        {
            var sqList = await _securityAnswerService.SaveSecurityAnswer(req);
            return Ok(sqList);
        }

        // PUT api/<SecurityQuestionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSecurityAnswer(SecurityAnswerUpdateModel req, Guid id)
        {
            var reportList = await _securityAnswerService.UpdateSecurityAnswer(req, id);
            return Ok(reportList);
        }

        // DELETE api/<SecurityQuestionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityAnswer(Guid id)
        {
            var reportList = await _securityAnswerService.DeleteSecurityAnswer(id);
            return Ok(reportList);
        }
    }
}