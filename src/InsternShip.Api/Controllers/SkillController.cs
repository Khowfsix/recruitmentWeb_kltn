using InsternShip.Data.ViewModels.Skill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class SkillController : BaseAPIController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkill(string? query)
        {
            var skilllist = await _skillService.GetAllSkills(query);
            if (skilllist == null)
            {
                return Ok("Not found");
            }

            return Ok(skilllist);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveSkill(SkillAddModel skillModel)
        {
            if (skillModel == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var skilllist = await _skillService.SaveSkill(skillModel);
            return Ok(skilllist);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateSkill(SkillUpdateModel skillModel, Guid id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var skillList = await _skillService.UpdateSkill(skillModel, id);
            return Ok(skillList);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            if (id != null)
            {
                var skillList = await _skillService.DeleteSkill(id);
                return Ok(skillList);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}