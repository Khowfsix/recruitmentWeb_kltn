using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SuccessfulCadidate;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace InsternShip.Data.Repositories
{
    public class SuccessfulCadidateRepository : Repository<SuccessfulCadidate>, ISuccessfulCadidateRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SuccessfulCadidateRepository(RecruitmentWebContext dbContext,
            IUnitOfWork uow,
            IMapper mapper) : base(dbContext)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SuccessfulCadidateModel>> GetAllSuccessfulCadidates(string? request)
        {
            var listDatas = new List<SuccessfulCadidateModel>();
            if (string.IsNullOrEmpty(request))
            {
                var datas = await Entities.Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<SuccessfulCadidateModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
            else
            {
                var datas = await Entities.Where(
                    s => s.Candidate.User.FullName.Contains(request) ||
                         s.Position.PositionName.Contains(request)
                    ).Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<SuccessfulCadidateModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
        }

        public async Task<SuccessfulCadidateModel> SaveSuccessfulCadidate(SuccessfulCadidateModel request)
        {
            var round = _mapper.Map<SuccessfulCadidate>(request);
            round.SuccessfulCadidateId = Guid.NewGuid();

            Entities.Add(round);
            _uow.SaveChanges();

            var response = _mapper.Map<SuccessfulCadidateModel>(round);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateSuccessfulCadidate(SuccessfulCadidateModel request, Guid requestId)
        {
            var round = _mapper.Map<SuccessfulCadidate>(request);
            round.SuccessfulCadidateId = requestId;

            Entities.Add(round);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSuccessfulCadidate(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.SuccessfulCadidateId == requestId);
            if (entity is null or { IsDeleted: true })
            {
                return await Task.FromResult(false);
            }
            entity.IsDeleted = true;
            Entities.Update(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}