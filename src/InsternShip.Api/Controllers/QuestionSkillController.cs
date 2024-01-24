using InsternShip.Data.ViewModels.QuestionSkill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{ 
    [Authorize]
    public class QuestionSkillController : BaseAPIController
    {
        private readonly IQuestionSkillService _questionSkillService;

        public QuestionSkillController(IQuestionSkillService questionSkillService)
        {
            _questionSkillService = questionSkillService;
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> AddQuestionSkill
        (QuestionSkillAddModel questionSkill)
        {
            var response = await _questionSkillService.AddQuestionSkill(questionSkill);

            return response is not null ?
            Ok(response) :
            BadRequest(questionSkill);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionSkills()
        {
            var response = await _questionSkillService.GetAllQuestionSkills();
            return Ok(response);
        }

        [HttpDelete("{questionSkillId:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> RemoveQuestionSkill(Guid questionSkillId)
        {
            var response = await _questionSkillService.RemoveQuestionSkill(questionSkillId);
            return response is true ? Ok(true) : NotFound(questionSkillId);
        }

        [HttpPut("{questionSkillId:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateQuestionSkill
        (QuestionSkillUpdateModel questionSkill, Guid questionSkillId)
        {
            var response =
                await _questionSkillService.UpdateQuestionSkill(questionSkill, questionSkillId);
            return response is true ? Ok(true) : NotFound(questionSkillId);
        }
    }
}