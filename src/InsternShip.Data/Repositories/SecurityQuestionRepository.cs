using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class SecurityQuestionRepository : Repository<SecurityQuestion>, ISecurityQuestionRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SecurityQuestionRepository
            (RecruitmentWebContext context,
        IUnitOfWork uow,
        IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<SecurityQuestionModel> AddSecurityQuestion(SecurityQuestionModel request)
        {
            var securityQuestion = _mapper.Map<SecurityQuestion>(request);
            securityQuestion.SecurityQuestionId = Guid.NewGuid();
            Entities.Add(securityQuestion);
            _uow.SaveChanges();

            var response = _mapper.Map<SecurityQuestionModel>(securityQuestion);

            return await Task.FromResult(response);
        }

        public async Task<List<SecurityQuestionModel>> GetSecurityQuestion()
        {
            var listData = new List<SecurityQuestionModel>();
            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<SecurityQuestionModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<bool> RemoveSecurityQuestion(Guid requestId)
        {
            var data = GetById(requestId);
            if (data != null)
            {
                Entities.Remove(data);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            throw new ArgumentNullException(nameof(data));
        }

        public async Task<bool> UpdateSecurityQuestion(SecurityQuestionModel request, Guid requestId)
        {
            var data = _mapper.Map<SecurityQuestion>(request);
            data.SecurityQuestionId = requestId;
            Entities.Update(data);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}