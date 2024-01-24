using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReportRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportModel>> GetAllReport()
        {
            var listData = new List<ReportModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<ReportModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<ReportModel> SaveReport(ReportModel request)
        {
            var report = _mapper.Map<Report>(request);
            report.ReportId = Guid.NewGuid();

            Entities.Add(report);
            _uow.SaveChanges();

            var response = _mapper.Map<ReportModel>(report);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateReport(ReportModel request, Guid requestId)
        {
            var report = _mapper.Map<Report>(request);
            report.ReportId = requestId;

            Entities.Update(report);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteReport(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.ReportId == requestId);
            if (entity is null or { IsDeleted: true })
            {
                return await Task.FromResult(false);
            }
            entity.IsDeleted = true;
            Entities.Update(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}