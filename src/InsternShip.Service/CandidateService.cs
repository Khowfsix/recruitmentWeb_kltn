using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Candidate;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCandidate(Guid requestId)
        {
            return await _candidateRepository.DeleteCandidate(requestId);
        }

        public async Task<IEnumerable<CandidateViewModel>> GetAllCandidates()
        {
            var data = await _candidateRepository.GetAllCandidates();
            List<CandidateViewModel> listData = new List<CandidateViewModel>();
            foreach (var candidate in data)
            {
                var obj = _mapper.Map<CandidateViewModel>(candidate);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<CandidateViewModel> SaveCandidate(CandidateAddModel request)
        {
            var data = _mapper.Map<CandidateModel>(request);
            var response = await _candidateRepository.SaveCandidate(data);

            return _mapper.Map<CandidateViewModel>(response);
        }

        public async Task<bool> UpdateCandidate(CandidateUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<CandidateModel>(request);
            return await _candidateRepository.UpdateCandidate(data, requestId);
        }

        public async Task<ProfileViewModel?> GetProfile(Guid candidateId)
        {
            var data = await _candidateRepository.GetProfile(candidateId);
            return _mapper.Map<ProfileViewModel>(data);
        }

        public async Task<CandidateViewModel> FindById(Guid id)
        {
            var model = await _candidateRepository.FindById(id);

            var viewmodel = _mapper.Map<CandidateViewModel>(model);

            return viewmodel;
        }

        public async Task<CandidateViewModel> GetCandidateByUserId(string id)
        {
            var candidateModel = await _candidateRepository.GetCandidateByUserId(id);
            var candidateVM = _mapper.Map<CandidateViewModel>(candidateModel);
            if (candidateVM != null)
                return candidateVM;
            else
            {
                return null;
            }
        }
    }
}