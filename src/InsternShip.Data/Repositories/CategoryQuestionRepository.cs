using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CategoryQuestionRepository
        : Repository<CategoryQuestion>,
            ICategoryQuestionRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryQuestionRepository(
            RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper
        )
            : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCategoryQuestion(Guid requestId)
        {
            try
            {
                var categoryQuestion = GetById(requestId);
                if (categoryQuestion != null)
                {
                    Entities.Remove(categoryQuestion);
                    _uow.SaveChanges();
                    return await Task.FromResult(true);
                }
                throw new ArgumentNullException(nameof(categoryQuestion));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryQuestionModel>> GetAllCategoryQuestions()
        {
            var listData = new List<CategoryQuestionModel>();
            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<CategoryQuestionModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<CategoryQuestionModel?> GetCategoryQuestionById(Guid id)
        {
            try
            {
                var categoryQuestion = GetById(id);
                var data = _mapper.Map<CategoryQuestionModel>(categoryQuestion);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryQuestionModel>> GetCategoryQuestionsByName(
            string keyword
        )
        {
            try
            {
                var listData = await Entities
                    .Where(
                        cq =>
                            cq.CategoryQuestionName != null
                            && cq.CategoryQuestionName.Contains(keyword)
                    )
                    .Select(
                        cq =>
                            new CategoryQuestionModel
                            {
                                CategoryQuestionId = cq.CategoryQuestionId,
                                CategoryQuestionName = cq.CategoryQuestionName,
                                Weight = cq.Weight
                            }
                    )
                    .ToListAsync();
                return listData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryQuestionModel>> GetCategoryQuestionsByWeight(
            double weight
        )
        {
            try
            {
                var listData = await Entities
                    .Where(cq => cq.Weight == weight)
                    .Select(
                        cq =>
                            new CategoryQuestionModel
                            {
                                CategoryQuestionId = cq.CategoryQuestionId,
                                CategoryQuestionName = cq.CategoryQuestionName,
                                Weight = cq.Weight
                            }
                    )
                    .ToListAsync();
                return listData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid> GetIdCategoryQuestion(string keyword)
        {
            var data = await Entities
                .Where(
                    cq =>
                        cq.CategoryQuestionName != null && cq.CategoryQuestionName.Contains(keyword)
                )
                .Select(
                    cq =>
                        new CategoryQuestionModel
                        {
                            CategoryQuestionId = cq.CategoryQuestionId,
                            CategoryQuestionName = cq.CategoryQuestionName,
                            Weight = cq.Weight
                        }
                )
                .FirstOrDefaultAsync();
            if (data != null)
                return data.CategoryQuestionId;
            else
                return Guid.Empty;
        }

        public async Task<CategoryQuestionModel> SaveCategoryQuestion(CategoryQuestionModel categoryQuestion)
        {
            var cateQuestion = _mapper.Map<CategoryQuestion>(categoryQuestion);
            cateQuestion.CategoryQuestionId = Guid.NewGuid();

            Entities.Add(cateQuestion);
            _uow.SaveChanges();

            var response = _mapper.Map<CategoryQuestionModel>(cateQuestion);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateCategoryQuestion(
            CategoryQuestionModel categoryQuestion,
            Guid categoryQuestionId
        )
        {
            //var cateQuest = Entities.First()
            var cateQuestion = _mapper.Map<CategoryQuestion>(categoryQuestion);
            cateQuestion.CategoryQuestionId = categoryQuestionId;
            Entities.Update(cateQuestion);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}