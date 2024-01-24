using InsternShip.Data.ViewModels.Language;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{ 
    [Authorize]
    public class LanguageController : BaseAPIController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages(Guid? languageId, string? languageName)
        {
            if (languageId != null)
            {
                var languageById = await _languageService.GetLanguage((Guid)languageId);
                if (languageById == null)
                {
                    return Ok();
                }
                return Ok(languageById);
            }
            else if (languageName != null)
            {
                var languagesByName = await _languageService.GetLanguage(languageName);
                if (languagesByName == null)
                {
                    return Ok();
                }
                return Ok(languagesByName);
            }

            var obj = await _languageService.GetAllLanguages();
            return Ok(obj);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter,Interviewer,Admin")]
        public async Task<IActionResult> AddLanguage(LanguageAddModel obj)
        {
            var response = await _languageService.AddLanguage(obj);
            return response is not null ? Ok(response)
                                        : BadRequest(obj);
        }

        [HttpPut("{languageId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLanguage(LanguageUpdateModel obj, Guid languageId)
        {
            var response = await _languageService.UpdateLanguage(obj, languageId);
            return response is true ? Ok(true) : NotFound(languageId);
        }

        [HttpDelete("{languageId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLanguage(Guid languageId)
        {
            var response = await _languageService.RemoveLanguage(languageId);
            return response is true ? Ok(true) : NotFound(languageId);
        }
    }
}