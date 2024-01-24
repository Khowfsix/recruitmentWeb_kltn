using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Round;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class RoundService : IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;

        public RoundService(IRoundRepository roundRepository, IMapper mapper)
        {
            _roundRepository = roundRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoundViewModel>> GetAllRounds(string? interviewId)
        {
            var modelDatas = await _roundRepository.GetAllRounds(interviewId);
            if (modelDatas != null)
            {
                List<RoundViewModel> list = new List<RoundViewModel>();
                foreach (var item in modelDatas)
                {
                    list.Add(_mapper.Map<RoundViewModel>(item));
                }
                return list;
            }
            return null;
        }

        public async Task<RoundViewModel> SaveRound(RoundAddModel roundModel)
        {
            var data = _mapper.Map<RoundModel>(roundModel);
            var response = await _roundRepository.SaveRound(data);
            return _mapper.Map<RoundViewModel>(response);
        }

        public async Task<bool> UpdateRound(RoundUpdateModel roundModel, Guid roundId)
        {
            var data = _mapper.Map<RoundModel>(roundModel);
            return await _roundRepository.UpdateRound(data, roundId);
        }

        public async Task<bool> DeleteRound(Guid roundId)
        {
            return await _roundRepository.DeleteRound(roundId);
        }

        public async Task<IEnumerable<RoundViewModel>> GetRoundsOfInterview(Guid interviewId)
        {
            var modelDatas = await _roundRepository.GetAllRounds(null);
            if (modelDatas != null)
            {
                List<RoundViewModel> list = new List<RoundViewModel>();
                foreach (var item in modelDatas)
                {
                    if (item.InterviewId.Equals(interviewId))
                    {
                        list.Add(_mapper.Map<RoundViewModel>(item));
                    }
                }
                return list;
            }
            return null;
        }
    }
}