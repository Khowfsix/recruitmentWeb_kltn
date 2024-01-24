using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoomRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomModel>> GetAllRoom()
        {
            var listData = new List<RoomModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<RoomModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<RoomModel> SaveRoom(RoomModel request)
        {
            var report = _mapper.Map<Room>(request);
            report.RoomId = Guid.NewGuid();

            Entities.Add(report);
            _uow.SaveChanges();

            var response = _mapper.Map<RoomModel>(report);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateRoom(RoomModel request, Guid requestId)
        {
            var report = _mapper.Map<Room>(request);
            report.RoomId = requestId;

            Entities.Update(report);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRoom(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.RoomId == requestId);
            if (entity is null or { IsDeleted: true })
            {
                return await Task.FromResult(false);
            }
            entity.IsDeleted = true;
            Entities.Update(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }
        public async Task<RoomModel> GetRoomById(Guid id)
        {
            var entity = await Entities.FindAsync(id);
            return entity is not null ? _mapper.Map<RoomModel>(entity) : null;
        }
    }
}