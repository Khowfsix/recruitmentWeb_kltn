using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Model;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CandidateRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCandidate(Guid candidateId)
        {
            try
            {
                var candidate = GetById(candidateId);
                if (candidate == null)
                    throw new ArgumentNullException(nameof(candidate));
                //Entities.Remove(candidate);
                candidate.IsDeleted = true;
                Entities.Update(candidate);

                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CandidateModel>> GetAllCandidates()
        {
            var listData = new List<CandidateModel>();

            var data = await Entities.Include(x => x.User).ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<CandidateModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<CandidateModel> FindById(Guid id)
        {
            var entity = await Entities
                .Include(c => c.User)
                .Where(x => x.CandidateId == id)
                .FirstOrDefaultAsync();

            var model = _mapper.Map<CandidateModel>(entity);

            return model;
        }

        public async Task<CandidateModel?> GetCandidateByUserId(string userId)
        {
            var data = await Entities
                .Where(x => x.UserId == userId)
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            var resp = _mapper.Map<CandidateModel>(data);

            return resp;
        }

        public async Task<ProfileModel?> GetProfile(Guid candidateId)
        {
            var profile = GetById(candidateId);
            if (profile is not null)
            {
                var obj = _mapper.Map<ProfileModel>(profile);
                return obj;
            }
            else return null;
        }

        public async Task<CandidateModel> SaveCandidate(CandidateModel request)
        {
            try
            {
                var candidate = _mapper.Map<Candidate>(request);
                candidate.CandidateId = Guid.NewGuid();

                Entities.Add(candidate);
                _uow.SaveChanges();

                var response = _mapper.Map<CandidateModel>(candidate);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCandidate(CandidateModel request, Guid requestId)
        {
            try
            {
                var candidate = _mapper.Map<Candidate>(request);
                candidate.CandidateId = requestId;
                Entities.Update(candidate);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}