using InsternShip.Data.Entities;
using InsternShip.Data.Models;

namespace InsternShip.Data.Interfaces
{
    public interface ICvRepository : IRepository<Cv>
    {
        Task<IEnumerable<CvModel>> GetAllCv(string? request);

        Task<IEnumerable<CvModel>> GetAllUserCv(string userId);

        Task<(bool, CvModel)> SaveCv(CvModel request);

        Task<bool> UpdateCv(CvModel request, Guid requestId);

        Task<bool> DeleteCv(Guid requestId);

        Task<IEnumerable<CvModel>> GetForeignKey(Guid requestId);

        Task<List<CvModel>> GetCvsByCandidateId(Guid candidateId);

        Task<CvModel> GetCVById(Guid id);

        //Task<CvModel> GetCVByIdNoTracking(Guid id);
    }
}