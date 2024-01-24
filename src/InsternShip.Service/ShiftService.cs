using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Shift;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IMapper _mapper;

        public ShiftService(IShiftRepository shiftRepository, IMapper mapper)
        {
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftViewModel>> GetAllShifts(int? request)
        {
            var modelDatas = await _shiftRepository.GetAllShifts(request);
            if (modelDatas != null)
            {
                List<ShiftViewModel> list = new List<ShiftViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<ShiftViewModel>(item));
                }
                return list;
            }
            return null;
        }

        public async Task<ShiftViewModel> SaveShift(ShiftAddModel request)
        {
            var data = _mapper.Map<ShiftModel>(request);
            var response = await _shiftRepository.SaveShift(data);
            return _mapper.Map<ShiftViewModel>(response);
        }

        public async Task<bool> UpdateShift(ShiftUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<ShiftModel>(request);
            return await _shiftRepository.UpdateShift(data, requestId);
        }

        public async Task<bool> DeleteShift(Guid requestId)
        {
            return await _shiftRepository.DeleteShift(requestId);
        }
    }
}