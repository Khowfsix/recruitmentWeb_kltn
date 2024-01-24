using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ResultRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResultModel>> GetAllResult()
        {
            var listData = new List<ResultModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<ResultModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<ResultModel> SaveResult(ResultModel request)
        {
            var report = _mapper.Map<Result>(request);
            report.ResultId = Guid.NewGuid();

            Entities.Add(report);
            _uow.SaveChanges();

            var response = _mapper.Map<ResultModel>(report);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateResult(ResultModel request, Guid requestId)
        {
            var report = _mapper.Map<Result>(request);
            request.ResultId = requestId;

            Entities.Update(report);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteResult(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.ResultId == requestId);
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
