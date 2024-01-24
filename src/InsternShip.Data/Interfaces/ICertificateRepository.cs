using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICertificateRepository
    {
        Task<IEnumerable<CertificateModel>> GetAllCertificate(string? request);

        Task<CertificateModel> SaveCertificate(CertificateModel request);

        Task<bool> UpdateCertificate(CertificateModel request, Guid requestId);

        Task<bool> DeleteCertificate(Guid requestId);

        Task<IEnumerable<CertificateModel>> GetForeignKey(Guid requestId);
    }
}