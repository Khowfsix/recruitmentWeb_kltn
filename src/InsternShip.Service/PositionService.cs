using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Position;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<PositionViewModel> AddPosition(PositionAddModel position)
        {
            var data = _mapper.Map<PositionModel>(position);
            var response = await _positionRepository.AddPosition(data);
            return _mapper.Map<PositionViewModel>(response);
        }

        public async Task<List<PositionViewModel>> GetAllPositions(Guid? departmentId)
        {
            var modelDatas = await _positionRepository.GetAllPositions();
            List<PositionViewModel> list = new List<PositionViewModel>();
            if (departmentId == null)
            {
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<PositionViewModel>(item));
                }
            }
            else
            {
                foreach (var item in modelDatas)
                {
                    if (item.DepartmentId.Equals(departmentId))
                    {
                        list.Add(_mapper.Map<PositionViewModel>(item));
                    }
                }
            }
            return list;
        }

        public async Task<PositionViewModel> GetPositionById(Guid id)
        {
            var data = await _positionRepository.GetPositionById(id);
            return _mapper.Map<PositionViewModel>(data);
        }

        public async Task<List<PositionViewModel>> GetPositionByName(string name)
        {
            var data = await _positionRepository.GetPositionByName(name);
            List<PositionViewModel> resultList = new();
            foreach (var result in data)
            {
                resultList.Add(_mapper.Map<PositionViewModel>(result));
            }
            return resultList;
        }

        public async Task<bool> RemovePosition(Guid position)
        {
            return await _positionRepository.RemovePosition(position);
        }

        public async Task<bool> UpdatePosition(PositionUpdateModel position, Guid positionId)
        {
            var data = _mapper.Map<PositionModel>(position);
            return await _positionRepository.UpdatePosition(data, positionId);
        }
    }
}