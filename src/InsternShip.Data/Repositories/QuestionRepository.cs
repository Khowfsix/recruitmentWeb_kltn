using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionRepository(IUnitOfWork unitOfWork,
            RecruitmentWebContext context,
            IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QuestionModel> AddQuestion(QuestionModel entity)
        {
            //throw new NotImplementedException();

            /*------------------------------*/
            // Adds mapped entity to db from given model.
            /*------------------------------*/
            var obj = _mapper.Map<Question>(entity);
            obj.QuestionId = Guid.NewGuid();

            await Entities.AddAsync(obj);
            _unitOfWork.SaveChanges();
            return await Task.FromResult(_mapper.Map<QuestionModel>(obj));
        }

        public async Task<List<QuestionModel>> GetAllQuestions()
        {
            /*------------------------------*/
            // Finds all of question entities asynchronously in db.
            // Returns a list of found questions in db.
            /*------------------------------*/
            var questionList = await Entities.Include(q => q.CategoryQuestion).ToListAsync();
            var resultList = new List<QuestionModel>();

            foreach (Question question in questionList)
            {
                var data = _mapper.Map<QuestionModel>(question);
                resultList.Add(data);
            }
            return resultList;
        }

        public async Task<List<QuestionModel>> GetListQuestions(Guid id)
        {
            var listData = new List<QuestionModel>();
            listData = await Entities.Include(q => q.CategoryQuestion)
            .Where(cq => cq.CategoryQuestionId == id)
            .Select(cq => new QuestionModel
            {
                QuestionId = cq.QuestionId,
                QuestionString = cq.QuestionString,
                CategoryQuestionId = cq.CategoryQuestionId
            }).ToListAsync();
            return listData;
        }

        public async Task<QuestionModel> GetQuestion(Guid? id)
        {
            /*------------------------------*/
            // Finds the first position entity that has the id asynchronously in db.
            // Returns it with the related entities if found. Otherwise, return null
            /*------------------------------*/

            var foundQuestion = await Entities.Include(q => q.CategoryQuestion).Where(q => q.QuestionId.Equals(id)).FirstOrDefaultAsync();

            // if (foundQuestion is not null)
            // {
            //     var data = _mapper.Map<QuestionModel>(foundQuestion);
            //     return data;
            // }
            // return null;

            /*------------------------------*/
            //Returns a QuestionModel mapped from foundQuestion if it is in db. Otherwise, return null.
            return foundQuestion is not null ? _mapper.Map<QuestionModel>(foundQuestion) : null;
            /*------------------------------*/
        }

        public async Task<List<QuestionModel>> GetQuestionsByName(string keyword)
        {
            // var listData = new List<Question>();
            // listData = await Entities
            // .Where(cq => cq.QuestionString.Contains(keyword))
            // .ToListAsync();

            //var response = new List<QuestionModel>();

            // if (listData != null)
            // {
            //     foreach(var item in listData)
            //     {
            //         var obj = _mapper.Map<QuestionModel>(item);
            //         response.Add(obj);
            //     }
            // }
            // if (response != null)
            //     return response;
            // else
            //     return null;

            /*------------------------------*/
            // Finds all of position entities that contain name parameter asynchronously in db.
            // Returns a list of it with the related entities if matched.
            var listData = new List<Question>();
            listData = await Entities
                    .Include(q => q.CategoryQuestion)
                    .Where(cq => cq.QuestionString.ToLower().Contains(keyword.ToLower().Trim()))
                    .ToListAsync();

            var resultList = new List<QuestionModel>();

            foreach (var item in listData)
            {
                var obj = _mapper.Map<QuestionModel>(item);
                resultList.Add(obj);
            }
            return resultList;
            /*------------------------------*/
        }

        public async Task<bool> RemoveQuestion(Guid id)
        {
            /*------------------------------*/
            // Finds asynchronously and removes entity with matched id in db.
            /*------------------------------*/
            var foundQuestion = await Entities.FindAsync(id);
            if (foundQuestion != null)
            {
                try
                {
                    Entities.Remove(foundQuestion);
                    _unitOfWork.SaveChanges();
                    return await Task.FromResult(true);
                }
                catch (Exception)
                {
                    await Task.FromResult(false);
                }
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> UpdateQuestion(QuestionModel entity, Guid id)
        {
            /*------------------------------*/
            // If id is not found in db, return false. Else, update and return true.
            if (await Entities.AnyAsync(l => l.QuestionId.Equals(id)) is false)
                return await Task.FromResult(false);
            /*------------------------------*/

            var updatedQuestion = _mapper.Map<Question>(entity);
            updatedQuestion.QuestionId = id;

            Entities.Update(updatedQuestion);
            _unitOfWork.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}