using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Room;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _reportRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<RoomViewModel> SaveRoom(RoomAddModel reportModel)
        {
            var data = _mapper.Map<RoomModel>(reportModel);
            var response = await _reportRepository.SaveRoom(data);
            return _mapper.Map<RoomViewModel>(response);
        }

        public async Task<bool> DeleteRoom(Guid reportModelId)
        {
            return await _reportRepository.DeleteRoom(reportModelId);
        }

        public async Task<IEnumerable<RoomViewModel>> GetAllRoom()
        {
            var modelDatas = await _reportRepository.GetAllRoom();
            List<RoomViewModel> list = new List<RoomViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<RoomViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateRoom(RoomUpdateModel reportModel, Guid reportModelId)
        {
            var data = _mapper.Map<RoomModel>(reportModel);
            return await _reportRepository.UpdateRoom(data, reportModelId);
        }
    }
}