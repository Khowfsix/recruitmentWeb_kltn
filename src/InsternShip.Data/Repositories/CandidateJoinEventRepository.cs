using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CandidateJoinEventRepository : Repository<CandidateJoinEvent>, ICandidateJoinEventRepository
    {
        private readonly List<CandidateJoinEventModel> _candidateJoinEvents = new List<CandidateJoinEventModel>();
        private RecruitmentWebContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CandidateJoinEventRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidateJoinEventModel>> GetAllCandidateJoinEvents()
        {
            var listData = new List<CandidateJoinEventModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<CandidateJoinEventModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<CandidateJoinEventModel> SaveCandidateJoinEvent(CandidateJoinEventModel request)
        {
            var candidateJoinEvent = _mapper.Map<CandidateJoinEvent>(request);
            candidateJoinEvent.CandidateJoinEventId = Guid.NewGuid();

            Entities.Add(candidateJoinEvent);
            _uow.SaveChanges();

            var response = _mapper.Map<CandidateJoinEventModel>(candidateJoinEvent);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateCandidateJoinEvent(CandidateJoinEventModel request, Guid requestId)
        {
            var candidateJoinEvent = _mapper.Map<CandidateJoinEvent>(request);
            candidateJoinEvent.CandidateJoinEventId = requestId;
            Entities.Update(candidateJoinEvent);
            _uow.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCandidateJoinEvent(Guid requestId)
        {
            try
            {
                var candidateJoinEvent = GetById(requestId);
                if (candidateJoinEvent == null)
                    throw new ArgumentNullException(nameof(candidateJoinEvent));
                Entities.Remove(candidateJoinEvent);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CandidateJoinEventModel>> JoinEventDetail(Guid candidateId)
        {
            try
            {
                var listData = await Entities
                    .Where(c => c.CandidateId.Equals(candidateId))
                    .Include(c => c.Event)
                    .Include(c => c.Candidate.User)
                    .ToListAsync();

                List<CandidateJoinEventModel> modelList = new List<CandidateJoinEventModel>();

                foreach (var item in listData)
                {
                    var obj = _mapper.Map<CandidateJoinEventModel>(item);
                    modelList.Add(obj);
                }
                return modelList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CandidateJoinEventModel>> GetCandidatesSortedByJoinEventCount()
        {
            return await Task.FromResult(_candidateJoinEvents.ToList());
        }
    }
}