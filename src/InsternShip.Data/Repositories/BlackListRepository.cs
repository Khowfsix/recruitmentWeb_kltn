using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class BlackListRepository : Repository<BlackList>, IBlacklistRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BlackListRepository(RecruitmentWebContext context, IUnitOfWork uow, IMapper mapper)
            : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBlackList(Guid blacklistId)
        {
            var blacklist = GetById(blacklistId);
            if (blacklist == null)
                throw new ArgumentNullException(nameof(blacklist));

            blacklist.IsDeleted = true;
            Entities.Update(blacklist);

            _uow.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<BlacklistModel>> GetAllBlackLists()
        {
            var listData = new List<BlacklistModel>();
            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<BlacklistModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<BlacklistModel> SaveBlackList(BlacklistModel request)
        {
            var blacklist = _mapper.Map<BlackList>(request);
            blacklist.BlackListId = Guid.NewGuid();

            Entities.Add(blacklist);
            _uow.SaveChanges();

            var response = _mapper.Map<BlacklistModel>(blacklist);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateBlackList(BlacklistModel request, Guid requestId)
        {
            var blacklist = _mapper.Map<BlackList>(request);
            blacklist.BlackListId = requestId;
            Entities.Update(blacklist);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> CheckIsInBlackList(Guid candidateId)
        {
            var data = await Entities.FirstOrDefaultAsync(x => x.CandidateId == candidateId);
            if (data != null)
                return true;
            else
                return false;
        }
    }
}