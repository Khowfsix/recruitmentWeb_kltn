using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Result;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _reportRepository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel> SaveResult(ResultAddModel reportModel)
        {
            var data = _mapper.Map<ResultModel>(reportModel);
            var response = await _reportRepository.SaveResult(data);
            return _mapper.Map<ResultViewModel>(response);
        }

        public async Task<bool> DeleteResult(Guid reportModelId)
        {
            return await _reportRepository.DeleteResult(reportModelId);
        }

        public async Task<IEnumerable<ResultViewModel>> GetAllResult()
        {
            var modelDatas = await _reportRepository.GetAllResult();
            List<ResultViewModel> list = new List<ResultViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<ResultViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateResult(ResultUpdateModel reportModel, Guid reportModelId)
        {
            var data = _mapper.Map<ResultModel>(reportModel);
            return await _reportRepository.UpdateResult(data, reportModelId);
        }
    }
}