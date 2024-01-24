using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LanguageRepository(RecruitmentWebContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LanguageModel> AddLanguage(LanguageModel createdLanguage)
        {
            /*------------------------------*/
            // Adds mapped entity to db from given model.
            /*------------------------------*/
            var obj = _mapper.Map<Language>(createdLanguage);
            obj.LanguageId = Guid.NewGuid();

            Entities.Add(obj);
            _unitOfWork.SaveChanges();
            return await Task.FromResult(_mapper.Map<LanguageModel>(obj));
        }

        public async Task<bool> RemoveLanguage(Guid id)
        {
            try
            {
                //var language = GetById(id);

                /*------------------------------*/
                // Finds asynchronously and removes entity with matched id in db.
                var language = await Entities.FindAsync(id);
                /*------------------------------*/

                if (language == null)
                    throw new ArgumentNullException(nameof(language));

                Entities.Remove(language);
                //language.IsDeleted = true;
                //Entities.Update(language);
                _unitOfWork.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LanguageModel>> GetAllLanguages()
        {
            /*------------------------------*/
            // Finds all of language entities asynchronously in db.
            // Returns a list of found entities.
            /*------------------------------*/
            var LanguagesList = await Entities.ToListAsync();
            var _list = new List<LanguageModel>();

            foreach (var language in LanguagesList)
            {
                var obj = _mapper.Map<LanguageModel>(language);
                _list.Add(obj);
            }
            return _list;
        }

        public async Task<LanguageModel> GetLanguage(Guid id)
        {
            // IAsyncEnumerable<Language> LanguageList = Entities.AsAsyncEnumerable();
            // LanguageModel result = new();
            // await foreach (var language in LanguageList)
            // {
            //     if (language.LanguageId == id)
            //     {
            //         result = _mapper.Map<LanguageModel>(language);
            //         break;
            //     }
            // }
            // return result;

            /*------------------------------*/
            // Finds asynchronously and returns the first language with matched id in db.
            // Returns null if id is not matched.
            var language = await Entities
                    .Where(l => l.LanguageId == id)
                    .FirstOrDefaultAsync();

            // Returns mapped model of the language if it is found. Otherwise, return null.
            return language is not null ? _mapper.Map<LanguageModel>(language) : null;
            /*------------------------------*/
        }

        public async Task<List<LanguageModel>> GetLanguage(string name)
        {
            // IAsyncEnumerable<Language> LanguagesList = Entities.AsAsyncEnumerable();
            // var _list = new List<LanguageModel>();

            // await foreach (var language in LanguagesList)
            // {
            //     if (language.LanguageName == name)
            //     {
            //         var obj = _mapper.Map<LanguageModel>(language);
            //         _list.Add(obj);
            //     }
            // }
            // return _list;

            /*------------------------------*/
            // Finds all of language entities that contain name parameter asynchronously in db.
            // Returns a list of models mapped from found entities if matched.

            var listLanguage = await Entities
                            .Where(l => l.LanguageName.ToLower().Contains(name.ToLower().Trim()))
                            .ToListAsync();

            var resultList = new List<LanguageModel>();

            foreach (var language in listLanguage)
            {
                resultList.Add(_mapper.Map<LanguageModel>(language));
            }
            return resultList;
            /*------------------------------*/
        }

        public async Task<bool> UpdateLanguage(LanguageModel createdLanguage, Guid id)
        {
            /*------------------------------*/
            // If id is not found in db, return false. Else, update and return true.
            if (await Entities.AnyAsync(l => l.LanguageId.Equals(id)) is false)
                return false;
            /*------------------------------*/

            var newLanguage = _mapper.Map<Language>(createdLanguage);
            newLanguage.LanguageId = id;

            Entities.Update(newLanguage);

            _unitOfWork.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}