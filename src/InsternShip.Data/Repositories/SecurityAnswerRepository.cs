using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SecurityAnswer;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class SecurityAnswerRepository : Repository<SecurityAnswer>, ISecurityAnswerRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SecurityAnswerRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SecurityAnswerModel>> GetAllSecurityAnswers()
        {
            var listData = new List<SecurityAnswerModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<SecurityAnswerModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<SecurityAnswerModel> SaveSecurityAnswer(SecurityAnswerModel request)
        {
            var report = _mapper.Map<SecurityAnswer>(request);
            report.SecurityAnswerId = Guid.NewGuid();

            Entities.Add(report);
            _uow.SaveChanges();

            var response = _mapper.Map<SecurityAnswerModel>(report);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateSecurityAnswer(SecurityAnswerModel request, Guid requestId)
        {
            var report = _mapper.Map<SecurityAnswer>(request);
            request.SecurityAnswerId = requestId;

            Entities.Update(report);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteSecurityAnswer(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.SecurityAnswerId == requestId);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}

