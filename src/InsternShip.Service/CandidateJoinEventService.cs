using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.CandidateJoinEvent;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class CandidateJoinEventService : ICandidateJoinEventService
    {
        private readonly ICandidateJoinEventRepository _candidateJoinEventRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public CandidateJoinEventService(ICandidateJoinEventRepository candidateJoinEventRepository,
            IEventRepository eventRepository,
            IMapper mapper)
        {
            _candidateJoinEventRepository = candidateJoinEventRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCandidateJoinEvent(Guid requestId)
        {
            return await _candidateJoinEventRepository.DeleteCandidateJoinEvent(requestId);
        }

        public async Task<IEnumerable<CandidateJoinEventViewModel>> GetAllCandidateJoinEvents()
        {
            var data = await _candidateJoinEventRepository.GetAllCandidateJoinEvents();
            if (data != null)
            {
                List<CandidateJoinEventViewModel> listData =
                    new List<CandidateJoinEventViewModel>();
                foreach (var dataItem in data)
                {
                    var obj = _mapper.Map<CandidateJoinEventViewModel>(dataItem);
                    listData.Add(obj);
                }
                return listData;
            }
            return null;
        }

        public async Task<CandidateJoinEventViewModel> SaveCandidateJoinEvent(CandidateJoinEventAddModel request)
        {
            var data = _mapper.Map<CandidateJoinEventModel>(request);
            var response = await _candidateJoinEventRepository.SaveCandidateJoinEvent(data);

            return _mapper.Map<CandidateJoinEventViewModel>(response);
        }

        public async Task<bool> UpdateCandidateJoinEvent(CandidateJoinEventUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<CandidateJoinEventModel>(request);
            return await _candidateJoinEventRepository.UpdateCandidateJoinEvent(data, requestId);
        }

        public async Task<IEnumerable<CandidateJoinedEvent>> JoinEventDetail(Guid id)
        {
            var data = await _candidateJoinEventRepository.JoinEventDetail(id);

            var result = _mapper.Map<List<CandidateJoinedEvent>>(data);

            return result;
        }

        public async Task<IEnumerable<CandidateJoinEventViewModel>> GetCandidatesSortedByJoinEventCount()
        {
            var candidateJoinEvents = await _candidateJoinEventRepository.GetAllCandidateJoinEvents();

            var candidateJoinEventViewModels = candidateJoinEvents
                .GroupBy(cje => cje.CandidateId)
                .Select(group => new CandidateJoinEventViewModel
                {
                    CandidateId = group.Key,
                    JoinEventCount = group.Count(),
                })
                .OrderBy(cjeViewModel => cjeViewModel.CandidateId) // Sorting by CandidateId as required.
                .ToList();

            return candidateJoinEventViewModels;
        }
    }
}