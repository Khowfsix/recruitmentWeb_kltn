using InsternShip.Data.ViewModels.CategoryQuestion;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class CategoryQuestionController : BaseAPIController
    {
        private readonly ICategoryQuestionService _categoryQuestionService;

        public CategoryQuestionController(ICategoryQuestionService categoryQuestionService)
        {
            _categoryQuestionService = categoryQuestionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryQuestions(Guid? id, string? name, double? weight)
        {
            if (id != null)
            {
                var categoryQuestion = await _categoryQuestionService.GetCategoryQuestionById((Guid)id);
                if (categoryQuestion == null)
                {
                    return Ok("Not found");
                }
                return Ok(categoryQuestion);
            }
            else if (name != null)
            {
                var listCategoryQuestionbyName = await _categoryQuestionService.GetCategoryQuestionsByName(name);
                if (listCategoryQuestionbyName == null)
                {
                    return Ok("Not found");
                }
                return Ok(listCategoryQuestionbyName);
            }
            else if (weight != null)
            {
                var listCategoryQuestionByWeight = await _categoryQuestionService.GetCategoryQuestionsByWeight((double)weight);
                if (listCategoryQuestionByWeight == null)
                {
                    return Ok("Not found");
                }
                return Ok(listCategoryQuestionByWeight);
            }

            var listCategoryQuestion = await _categoryQuestionService.GetAllCategoryQuestions();
            return Ok(listCategoryQuestion);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryQuestionById(Guid id)
        {
            var categoryQuestion = await _categoryQuestionService.GetCategoryQuestionById(id);
            if (categoryQuestion == null)
            {
                return Ok("Not found");
            }
            return Ok(categoryQuestion);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryQuestionsByName(string keyword)
        {
            var listCategoryQuestion = await _categoryQuestionService.GetCategoryQuestionsByName(keyword);
            return Ok(listCategoryQuestion);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryQuestionsByWeight(double weight)
        {
            var listCategoryQuestion = await _categoryQuestionService.GetCategoryQuestionsByWeight(weight);
            if (listCategoryQuestion == null)
            {
                return Ok("Not found");
            }
            return Ok(listCategoryQuestion);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> SaveCategoryQuestion(CategoryQuestionAddModel categoryQuestion)
        {
            if (categoryQuestion == null)
            {
                return Ok("Not found");
            }
            var listCategoryQuestion = await _categoryQuestionService.SaveCategoryQuestion(categoryQuestion);
            return Ok(listCategoryQuestion);
        }

        [HttpPut("{categoryQuestionId:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> UpdateCategoryQuestion(CategoryQuestionUpdateModel categoryQuestion, Guid categoryQuestionId)
        {
            if (categoryQuestion == null)
            {
                return Ok("Not found");
            }
            var listCategoryQuestion = await _categoryQuestionService.UpdateCategoryQuestion(categoryQuestion, categoryQuestionId);
            return Ok(listCategoryQuestion);
        }

        [HttpDelete("{requestId:guid}")]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> DeleteCategoryQuestion(Guid requestId)
        {
            if (requestId == null)
            {
                return Ok("Not found");
            }
            var listCategoryQuestion = await _categoryQuestionService.DeleteCategoryQuestion(requestId);
            return Ok(listCategoryQuestion);
        }
    }
}