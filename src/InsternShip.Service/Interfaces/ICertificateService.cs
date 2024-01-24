using InsternShip.Data.ViewModels.Certificate;

namespace InsternShip.Service.Interfaces
{
    public interface ICertificateService
    {
        Task<IEnumerable<CertificateViewModel>> GetAllCertificate(string? request);

        Task<CertificateViewModel> SaveCertificate(CertificateAddModel request);

        Task<bool> UpdateCertificate(CertificateUpdateModel request, Guid requestId);

        Task<bool> DeleteCertificate(Guid requestId);
    }
}