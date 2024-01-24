using InsternShip.Data.ViewModels.CvHasSkill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class CvHasSkillController : BaseAPIController
    {
        private readonly ICvHasSkillService _cvHasSkillService;

        public CvHasSkillController(ICvHasSkillService cvHasSkillService)
        {
            _cvHasSkillService = cvHasSkillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCvHasSkill(string? request)
        {
            var cvHasSkillList = await _cvHasSkillService.GetAllCvHasSkillService(request);
            if (cvHasSkillList == null)
            {
                return Ok("Not found");
            }
            return Ok(cvHasSkillList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCvHasSkill(CvHasSkillAddModel request)
        {
            var cvHasSkillList = await _cvHasSkillService.SaveCvHasSkillService(request);
            if (cvHasSkillList == null)
            {
                return Ok("Not found");
            }

            return Ok(cvHasSkillList);
        }

        [HttpPut("{requestId:guid}")]
        public async Task<IActionResult> UpdateCvHasSkill(CvHasSkillUpdateModel request, Guid requestId)
        {
            var cvHasSkillList = await _cvHasSkillService.UpdateCvHasSkillService(request, requestId);
            if (cvHasSkillList == null)
            {
                return Ok("Not found");
            }
            return Ok(cvHasSkillList);
        }

        [HttpDelete("{requestId:guid}")]
        public async Task<IActionResult> DeleteCvHasSkill(Guid requestId)
        {
            var cvHasSkillList = await _cvHasSkillService.DeleteCvHasSkillService(requestId);
            if (cvHasSkillList == null)
            {
                return Ok("Not found");
            }
            return Ok(cvHasSkillList);
        }
    }
}