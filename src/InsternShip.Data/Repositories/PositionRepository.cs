using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionRepository(RecruitmentWebContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PositionModel> AddPosition(PositionModel position)
        {
            /*------------------------------*/
            // Adds mapped entity to db from given model.
            /*------------------------------*/
            var obj = _mapper.Map<Position>(position);
            obj.PositionId = Guid.NewGuid();

            Entities.Add(obj);
            _unitOfWork.SaveChanges();

            return await Task.FromResult(_mapper.Map<PositionModel>(obj));
        }

        public async Task<List<PositionModel>> GetAllPositions()
        {
            /*------------------------------*/
            // Finds all of position entities asynchronously in db.
            // Returns a list of it with the related entities.
            /*------------------------------*/
            var positionListWithName = await Entities
                .Include(o => o.Requirements)
                .Include(o => o.Department)
                .Include(o => o.Language)
                .Include(o => o.Recruiter)
                .ToListAsync();
            var resultListWithName = _mapper.Map<List<PositionModel>>(positionListWithName);
            return resultListWithName;
        }

        public async Task<PositionModel> GetPositionById(Guid id)
        {
            /*------------------------------*/
            // Finds the first position entity that has the id asynchronously in db.
            // Returns it with the related entities if found. Otherwise, return null
            /*------------------------------*/
            var position = await Entities
                .Where(p => p.PositionId == id)
                .Include(o => o.Requirements)
                .Include(o => o.Department)
                .Include(o => o.Language)
                .Include(o => o.Recruiter)
                .FirstOrDefaultAsync();

            // if (position is not null)
            // {
            //     var obj = _mapper.Map<PositionModel>(position);
            //     return obj;
            // }
            // else return null;

            /*------------------------------*/
            // Returns mapped model of the found position. If position is not found, return null.
            return position is not null ? _mapper.Map<PositionModel>(position) : null;
            /*------------------------------*/
        }

        public async Task<List<PositionModel>> GetPositionByName(string name)
        {
            /*------------------------------*/
            // Finds all of position entities that contain name parameter asynchronously in db.
            // Returns a list of it with the related entities if matched.
            /*------------------------------*/

            var positionList = await Entities
                .Where(p => p.PositionName.ToLower().Contains(name.ToLower().Trim()))
                .Include(o => o.Requirements)
                .Include(o => o.Department)
                .Include(o => o.Language)
                .Include(o => o.Recruiter)
                .ToListAsync();
            var resultList = new List<PositionModel>();

            foreach (var position in positionList)
            {
                var data = _mapper.Map<PositionModel>(position);
                resultList.Add(data);
            }
            return resultList;
        }

        public async Task<bool> RemovePosition(Guid positionId)
        {
            try
            {
                //var position = GetById(positionId);

                /*------------------------------*/
                // Finds asynchronously and removes entity with matched id in db.
                var position = await Entities.FindAsync(positionId);
                /*------------------------------*/

                if (position == null)
                {
                    return await Task.FromResult(false);
                    //throw new ArgumentNullException(nameof(position));
                }

                //Entities.Remove(position);
                position.IsDeleted = true;
                Entities.Update(position);

                _unitOfWork.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdatePosition(PositionModel position, Guid positionId)
        {
            /*------------------------------*/
            // If id is not found in db, return false. Else, update in db and return true.
            if (await Entities.AnyAsync(l => l.PositionId.Equals(positionId)) is false)
                return await Task.FromResult(false);
            /*------------------------------*/

            var updateData = _mapper.Map<Position>(position);
            updateData.PositionId = positionId;

            Entities.Update(updateData);
            _unitOfWork.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}