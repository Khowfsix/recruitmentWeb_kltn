using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Shift;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ShiftRepository(RecruitmentWebContext dbContext,
            IUnitOfWork uow,
            IMapper mapper) : base(dbContext)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftModel>> GetAllShifts(int? request)
        {
            var listDatas = new List<ShiftModel>();
            if (request == null)
            {
                var datas = await Entities.Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<ShiftModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
            else
            {
                var datas = await Entities.Where(s => s.ShiftTimeStart == request || s.ShiftTimeEnd == request)
                                          .Take(10).ToListAsync();
                foreach (var data in datas)
                {
                    var obj = _mapper.Map<ShiftModel>(data);
                    listDatas.Add(obj);
                }
                return listDatas;
            }
        }

        public async Task<ShiftModel> SaveShift(ShiftModel request)
        {
            var round = _mapper.Map<Shift>(request);
            round.ShiftId = Guid.NewGuid();

            Entities.Add(round);
            _uow.SaveChanges();

            var response = _mapper.Map<ShiftModel>(round);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateShift(ShiftModel request, Guid requestId)
        {
            var round = _mapper.Map<Shift>(request);
            round.ShiftId = requestId;

            Entities.Update(round);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteShift(Guid requestId)
        {
            var data = GetById(requestId);
            try
            {
                if (data != null)
                {
                    Entities.Remove(data);
                    _uow.SaveChanges();

                    return await Task.FromResult(true);
                }
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
                //throw new ArgumentNullException(nameof(data));
            }
            return await Task.FromResult(false);
        }
    }
}