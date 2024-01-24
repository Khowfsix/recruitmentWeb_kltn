using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Language;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository = null!;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<LanguageViewModel> AddLanguage(LanguageAddModel createdLanguage)
        {
            var data = _mapper.Map<LanguageModel>(createdLanguage);
            var response = await _languageRepository.AddLanguage(data);
            return _mapper.Map<LanguageViewModel>(response);
        }

        public async Task<bool> RemoveLanguage(Guid id)
        {
            return await _languageRepository.RemoveLanguage(id);
        }

        public async Task<List<LanguageViewModel>> GetAllLanguages()
        {
            var modelDatas = await _languageRepository.GetAllLanguages();
            List<LanguageViewModel> list = new();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<LanguageViewModel>(item));
            }
            return list;
        }

        public async Task<LanguageViewModel> GetLanguage(Guid id)
        {
            var data = await _languageRepository.GetLanguage(id);
            return _mapper.Map<LanguageViewModel>(data);
        }

        public async Task<List<LanguageViewModel>> GetLanguage(string name)
        {
            var modelDatas = await _languageRepository.GetLanguage(name);
            List<LanguageViewModel> list = new List<LanguageViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<LanguageViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateLanguage(LanguageUpdateModel createdLanguage, Guid id)
        {
            var data = _mapper.Map<LanguageModel>(createdLanguage);
            return await _languageRepository.UpdateLanguage(data, id);
        }
    }
}