using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoundRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoundModel>> GetAllRounds(string? request)
        {
            var listDatas = new List<RoundModel>();
            if (string.IsNullOrEmpty(request))
            {
                var datas = await Entities.ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<RoundModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
            else
            {
                var datas = await Entities.Where(r => r.InterviewId.ToString().Contains(request)).Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<RoundModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
        }

        public async Task<RoundModel> SaveRound(RoundModel request)
        {
            var round = _mapper.Map<Round>(request);
            round.RoundId = Guid.NewGuid();

            Entities.Add(round);
            _uow.SaveChanges();

            var response = _mapper.Map<RoundModel>(round);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateRound(RoundModel request, Guid requestId)
        {
            var round = _mapper.Map<Round>(request);
            round.RoundId = requestId;

            Entities.Update(round);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRound(Guid requestId)
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
    }
}