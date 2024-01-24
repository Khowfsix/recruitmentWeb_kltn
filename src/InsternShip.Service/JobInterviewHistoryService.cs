using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Application;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InsternShip.Service
{
    public class JobInterviewHistoryService : IJobInterviewHistoryService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ICvRepository _cvRepository;
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IItrsinterviewRepository _itrsinterviewRepository;
        private readonly IInterviewRepository _interviewRepository;

        public JobInterviewHistoryService(IApplicationRepository applicationRepository,
        IRoomRepository roomRepository, IPositionRepository positionRepository,
        ICvRepository cvRepository, IInterviewerRepository interviewerRepository,
        IItrsinterviewRepository itrsinterviewRepository, ICandidateRepository candidateRepository
        , IInterviewRepository interviewRepository)
        {
            _applicationRepository = applicationRepository;
            _roomRepository = roomRepository;
            _positionRepository = positionRepository;
            _cvRepository = cvRepository;
            _interviewerRepository = interviewerRepository;
            _itrsinterviewRepository = itrsinterviewRepository;
            _candidateRepository = candidateRepository;
            _interviewRepository = interviewRepository;
        }

        public async Task<List<ApplicationHistoryViewModel>> GetApplicationHistory
        (Guid candidateId)

        {
            // var currentUserId =
            //    ClaimsPrincipal.Current.FindFirst(i => i.Type == ClaimTypes.Name).Value;

            // var currentCandidateId = _candidateRepository.GetCandidateByUserId(currentUserId);

            var cvList = await _cvRepository.GetCvsByCandidateId(candidateId);
            foreach (var cv in cvList)
            {
                Console.WriteLine(cv.Cvid);
            }
            List<ApplicationHistoryViewModel> result = new();
            foreach (var cv in cvList)
            {
                var applicationHistory = await _applicationRepository.GetApplicationHistory(cv.Cvid);
                Console.WriteLine(applicationHistory);
                foreach (var application in applicationHistory)
                {
                    Console.WriteLine(application.ApplicationId);
                    result.Add(application);
                }
            }
            return result.AsEnumerable().OrderByDescending(application => application.DateTime).ToList();
            //return listCandidates.FirstOrDefault(candidate => candidate.CandidateId == candidateId);
        }

        public async Task<CvModel> GetCV(Guid Cvid)
        {
            var cvList = await _cvRepository.GetAllCv("");
            return cvList.FirstOrDefault(cv => cv.Cvid == Cvid);
        }

        public async Task<InterviewerModel> GetInterviewerInformation(Guid applicationId)
        {
            return await _interviewerRepository.GetInterviewerById(applicationId);
        }

        public async Task<PositionModel> GetPosition(Guid applicationId)
        {
            return await _positionRepository.GetPositionById(applicationId);
        }

        public async Task<RoomModel> GetRoomInformation(Guid applicationId)
        {
            var interviewSetForApplication =
                await _interviewRepository.GetInterviewById(applicationId);

            var itrsForInterview =
                await _itrsinterviewRepository.GetItrsinterviewById
                (interviewSetForApplication.ItrsinterviewId.Value);

            return await _roomRepository.GetRoomById(itrsForInterview.RoomId);
        }
    }
}