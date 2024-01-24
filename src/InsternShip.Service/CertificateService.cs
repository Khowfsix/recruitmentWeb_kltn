using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;

        public CertificateService(ICertificateRepository certificateRepository, IMapper mapper)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCertificate(Guid requestId)
        {
            return await _certificateRepository.DeleteCertificate(requestId);
        }

        public async Task<IEnumerable<CertificateViewModel>> GetAllCertificate(string? request)
        {
            var data = await _certificateRepository.GetAllCertificate(request);
            if (data != null)
            {
                List<CertificateViewModel> result = new List<CertificateViewModel>();
                foreach (var item in data)
                {
                    var obj = _mapper.Map<CertificateViewModel>(item);
                    result.Add(obj);
                }
                return result;
            }
            return null;
        }

        public async Task<CertificateViewModel> SaveCertificate(CertificateAddModel request)
        {
            var data = _mapper.Map<CertificateModel>(request);
            var response = await _certificateRepository.SaveCertificate(data);

            return _mapper.Map<CertificateViewModel>(response);
        }

        public async Task<bool> UpdateCertificate(CertificateUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<CertificateModel>(request);
            return await _certificateRepository.UpdateCertificate(data, requestId);
        }
    }
}