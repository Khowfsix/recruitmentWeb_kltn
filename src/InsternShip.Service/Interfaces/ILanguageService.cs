using InsternShip.Data.ViewModels.Language;

namespace InsternShip.Service.Interfaces
{
    public interface ILanguageService
    {
        Task<LanguageViewModel> AddLanguage(LanguageAddModel createdLanguage);

        Task<bool> UpdateLanguage(LanguageUpdateModel createdLanguage, Guid id);

        Task<bool> RemoveLanguage(Guid id);

        Task<List<LanguageViewModel>> GetAllLanguages();

        Task<LanguageViewModel> GetLanguage(Guid id);

        Task<List<LanguageViewModel>> GetLanguage(string name);

        //Task<bool> LanguageExists(Guid id);
    }
}