using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CertificateRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCertificate(Guid requestId)
        {
            try
            {
                var certificate = GetById(requestId);
                if (certificate == null)
                    throw new ArgumentNullException(nameof(certificate));
                Entities.Remove(certificate);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CertificateModel>> GetAllCertificate(string? request)
        {
            try
            {
                var listData = new List<CertificateModel>();
                if (string.IsNullOrEmpty(request))
                {
                    var data = await Entities.ToListAsync();
                    foreach (var item in data)
                    {
                        var obj = _mapper.Map<CertificateModel>(item);
                        listData.Add(obj);
                    }
                }
                else
                {
                    var data = await Entities
                        .Where(rp => rp.CertificateName.Contains(request))
                        .Select(rp => new CertificateModel
                        {
                            CertificateId = rp.CertificateId,
                            CertificateName = rp.CertificateName,
                            Description = rp.Description,
                            OrganizationName = rp.OrganizationName,
                            DateEarned = rp.DateEarned,
                            ExpirationDate = rp.ExpirationDate,
                            Cvid = rp.Cvid,
                            //IsDeleted = rp.IsDeleted,
                        }).ToListAsync();
                    return data;
                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CertificateModel>> GetForeignKey(Guid requestId)
        {
            var data = await Entities
                .Where(x => x.Cvid == requestId)
                .ToListAsync();

            var resp = _mapper.Map<IEnumerable<CertificateModel>>(data);

            return resp;
        }

        public async Task<CertificateModel> SaveCertificate(CertificateModel request)
        {
            try
            {
                var certificate = _mapper.Map<Certificate>(request);
                certificate.CertificateId = Guid.NewGuid();

                Entities.Add(certificate);
                _uow.SaveChanges();

                var response = _mapper.Map<CertificateModel>(certificate);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<bool> UpdateCertificate(CertificateModel request, Guid requestId)
        {
            try
            {
                var certificate = _mapper.Map<Certificate>(request);
                certificate.CertificateId = requestId;
                Entities.Update(certificate);
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