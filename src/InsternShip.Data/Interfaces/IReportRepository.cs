using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Report;

namespace InsternShip.Data.Interfaces
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<IEnumerable<ReportModel>> GetAllReport();
        Task<ReportModel> SaveReport(ReportModel request);
        Task<bool> UpdateReport(ReportModel request, Guid requestId);
        Task<bool> DeleteReport(Guid requestId);
    }
}
