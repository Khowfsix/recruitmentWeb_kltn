using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CvRepository : Repository<Cv>, ICvRepository
    {
        #region mapping
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUploadFileRepository _uploadFileRepository;
        #endregion

        #region Constructor
        public CvRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper,
            ICandidateRepository candidateRepository,
            IUploadFileRepository uploadFileRepository) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
            _candidateRepository = candidateRepository;
            _uploadFileRepository = uploadFileRepository;
        }
        #endregion

        public async Task<bool> DeleteCv(Guid requestId)
        {
            try
            {
                var cv = GetById(requestId);
                if (cv == null)
                    throw new ArgumentNullException(nameof(cv));

                if (cv.IsDeleted)
                    return false;

                cv.IsDeleted = true;

                Entities.Update(cv);
                var changes = _uow.SaveChanges();

                return await Task.FromResult(changes > 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CvModel> GetCVById(Guid id)
        {
            var cv = Entities
                .AsNoTracking()
                .Include(c => c.Candidate)
                .Where(c => c.Cvid == id)
                .FirstOrDefault();
            var data = _mapper.Map<CvModel>(cv);
            return data;
        }

        public async Task<IEnumerable<CvModel>> GetAllCv(string? request)
        {
            try
            {
                var listData = new List<CvModel>();
                if (string.IsNullOrEmpty(request))
                {
                    var data = await Entities.ToListAsync();
                    foreach (var item in data)
                    {
                        var obj = _mapper.Map<CvModel>(item);
                        listData.Add(obj);
                    }
                }
                else
                {
                    var data = await Entities
                        .Where(rp => rp.CvName.Contains(request))
                        .ToListAsync();

                    var resp = _mapper.Map<List<CvModel>>(data);

                    return resp;
                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(bool, CvModel)> SaveCv(CvModel request)
        {
            try
            {
                request.Cvid = Guid.NewGuid();
                var cv = _mapper.Map<Cv>(request);

                var result = Entities.Add(cv);
                _uow.SaveChanges();

                return (await Task.FromResult(true), request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<bool> UpdateCv(CvModel request, Guid requestId)
        {
            try
            {
                var cvPdf_old = Entities.AsNoTracking().Where(c => c.Cvid == requestId).FirstOrDefault();

                var cv = _mapper.Map<Cv>(request);
                Entities.Update(cv);

                if (cvPdf_old != null && (cvPdf_old.CvPdf.Trim() != "" || cvPdf_old.CvPdf != null))
                {
                    var del = _uploadFileRepository.DeleteFileAsync(cvPdf_old.CvPdf);
                }

                var changes = _uow.SaveChanges();

                return await Task.FromResult(changes > 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CvModel>> GetForeignKey(Guid requestId)
        {
            var data = await Entities
                .Where(x => x.CandidateId == requestId)
                .Where(x => x.IsDeleted == false)
                .ToListAsync();

            var resp = _mapper.Map<List<CvModel>>(data);

            return resp;
        }

        public async Task<List<CvModel>> GetCvsByCandidateId(Guid candidateId)
        {
            var cvList = await Entities
                        .Where(cv => cv.CandidateId == candidateId)
                        .Select(
                            cv => _mapper.Map<CvModel>(cv)
                        ).ToListAsync();
            return cvList;
        }

        public async Task<IEnumerable<CvModel>> GetAllUserCv(string userId)
        {
            var candidate = await _candidateRepository.GetCandidateByUserId(userId);
            var data = await Entities
                .Where(x => x.CandidateId == candidate.CandidateId)
                .ToListAsync();

            var resp = _mapper.Map<IEnumerable<CvModel>>(data);

            return resp;
        }
    }
}