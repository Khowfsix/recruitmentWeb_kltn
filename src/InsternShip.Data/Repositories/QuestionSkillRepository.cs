using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class QuestionSkillRepository : Repository<QuestionSkill>, IQuestionSkillRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionSkillRepository(RecruitmentWebContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QuestionSkillModel> AddQuestionSkill(QuestionSkillModel questionSkill)
        {
            /*------------------------------*/
            // Adds mapped entity to db from given model.
            /*------------------------------*/
            var newQuesSkill = _mapper.Map<QuestionSkill>(questionSkill);
            newQuesSkill.QuestionSkillsId = Guid.NewGuid();

            Entities.Add(newQuesSkill);
            _unitOfWork.SaveChanges();
            return await Task.FromResult(_mapper.Map<QuestionSkillModel>(newQuesSkill));
        }

        public async Task<List<QuestionSkillModel>> GetAllQuestionSkills()
        {
            /*------------------------------*/
            // Finds all of questionSkill entities asynchronously in db.
            // Returns a list of found questionSkills in db.
            /*------------------------------*/
            var questionSkillList = await Entities.ToListAsync();
            var resultList = new List<QuestionSkillModel>();
            foreach (var question in questionSkillList)
            {
                var data = _mapper.Map<QuestionSkillModel>(question);
                resultList.Add(data);
            }
            return resultList;
        }

        public async Task<bool> RemoveQuestionSkill(Guid id)
        {
            //var foundQuestionSkill = GetById(id);

            /*------------------------------*/
            // Finds asynchronously and removes entity with matched id in db.
            var foundQuestionSkill = await Entities.FindAsync(id);
            /*------------------------------*/

            if (foundQuestionSkill is not null)
            {
                Entities.Remove(foundQuestionSkill);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestionSkill(QuestionSkillModel questionSkill, Guid id)
        {
            /*------------------------------*/
            // If id is not found in db, return false. Else, update and return true.
            if (await Entities.AnyAsync(l => l.QuestionSkillsId.Equals(id)) is false)
                return await Task.FromResult(false);
            /*------------------------------*/

            var updatedData = _mapper.Map<QuestionSkill>(questionSkill);
            updatedData.QuestionSkillsId = id;

            Entities.Update(updatedData);
            _unitOfWork.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}