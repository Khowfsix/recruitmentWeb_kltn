using InsternShip.Data.Entities;
using Microsoft.Identity.Client;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Language;

namespace InsternShip.Data.Interfaces
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<LanguageModel> AddLanguage(LanguageModel createdLanguage);
        Task<bool> UpdateLanguage(LanguageModel createdLanguage, Guid id);
        Task<bool> RemoveLanguage(Guid id);
        Task<List<LanguageModel>> GetAllLanguages();
        Task<LanguageModel> GetLanguage(Guid id);
        Task<List<LanguageModel>> GetLanguage(string name);
        //Task<bool> LanguageExists(Guid id);
    }
}