using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Application;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICvRepository _cvRepository;
        private readonly IBlacklistRepository _blacklistRepository;
        private readonly IMapper _mapper;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            ICvRepository cvRepository,
            IBlacklistRepository blacklistRepository,
            IMapper mapper
        )
        {
            _applicationRepository = applicationRepository;
            _cvRepository = cvRepository;
            _blacklistRepository = blacklistRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteApplication(Guid applicationId)
        {
            return await _applicationRepository.DeleteApplication(applicationId);
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAllApplications()
        {
            var data = await _applicationRepository.GetAllApplications();
            List<ApplicationViewModel> listData = new();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var obj = _mapper.Map<ApplicationViewModel>(item);
                    listData.Add(obj);
                }
                return listData;
            }
            return null;
        }

        //public async Task<bool> SaveApplication(ApplicationAddModel request)
        //{
        //    var candidateId = await _cvRepository.GetCandidateIdByCVId(request.CvId);
        //    var isInBlackList = await _blacklistRepository.CheckIsInBlackList(candidateId);
        //    var data = _mapper.Map<ApplicationModel>(request);
        //    if (isInBlackList)
        //    {
        //        data.Company_Status = "Rejected";
        //    }
        //    return await _applicationRepository.SaveApplication(data);
        //}

        //public async Task<bool> UpdateApplication(ApplicationUpdateModel request, Guid candidateId)
        //{
        //    var data = _mapper.Map<ApplicationModel>(request);
        //    return await _applicationRepository.UpdateApplication(data, candidateId);
        //}

        //public async Task<IEnumerable<ApplicationHistoryViewModel>> GetApplicationHistory(
        //    Guid candidateId
        //)
        //{
        //    return await _applicationRepository.GetApplicationHistory(candidateId);
        //}

        public async Task<ApplicationViewModel?> GetApplicationById(Guid ApplicationId)
        {
            var modelData = await _applicationRepository.GetApplicationById(ApplicationId);
            if (modelData != null)
            {
                var data = _mapper.Map<ApplicationViewModel>(modelData);
                return data;
            }
            return null!;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAllApplicationsOfPosition(Guid? positionId, string? status, string? priority)
        {
            var modelDatas = await _applicationRepository.GetAllApplications();
            if (status == null) status = "";
            if (priority == null) priority = "";

            List<ApplicationViewModel> listData = new List<ApplicationViewModel>();
            if (modelDatas != null)
            {
                foreach (var item in modelDatas)
                {
                    if (item.Position.PositionId.Equals(positionId) &&
                        (item.Company_Status.Contains(status) || item.Candidate_Status.Contains(status)) &&
                        item.Priority.Contains(priority))
                    {
                        listData.Add(_mapper.Map<ApplicationViewModel>(item));
                    }
                }
            }
            return listData;
        }

        //public async Task<IEnumerable<ApplicationViewModel>> GetAllApplicationsOfPosition(Guid? positionId)
        //{
        //    var data = await _applicationRepository.GetAllApplications();
        //    List<ApplicationViewModel> listData = new List<ApplicationViewModel>();
        //    if (data != null)
        //    {
        //        foreach (var item in data)
        //        {
        //            var obj = _mapper.Map<ApplicationViewModel>(item);
        //            listData.Add(obj);
        //        }
        //        return listData;
        //    }
        //    return null;
        //}

        public async Task<ApplicationViewModel> SaveApplication(ApplicationAddModel request)
        {
            var data = _mapper.Map<ApplicationModel>(request);
            var blackList = await _blacklistRepository.GetAllBlackLists();
            var thisCv = await _cvRepository.GetCVById(request.Cvid);
            var canInBlacklist = blackList.Where(c => c.CandidateId == thisCv.CandidateId).ToList();

            // Add auto check candidate in blacklist ??
            if (canInBlacklist.Count > 0)
            {
                //candidate_status is "pending" default
                data.Company_Status = "Rejected";
            }

            var response = await _applicationRepository.SaveApplication(data);
            return _mapper.Map<ApplicationViewModel>(response);
        }

        public async Task<bool> UpdateApplication(ApplicationUpdateModel request, Guid applicationId)
        {
            var data = _mapper.Map<ApplicationModel>(request);

            return await _applicationRepository.UpdateApplication(data, applicationId);
        }

        public async Task<IEnumerable<ApplicationHistoryViewModel>> GetApplicationHistory(Guid candidateId)
        {
            return await _applicationRepository.GetApplicationHistory(candidateId);
        }

        public async Task<IEnumerable<ApplicationViewModel>>? GetApplicationsWithStatus(string status, string priority)
        {
            var data = await _applicationRepository.GetApplicationsWithStatus(status, priority);
            return _mapper.Map<List<ApplicationViewModel>>(data);
        }

        public async Task<bool> UpdateStatusApplication(Guid applicationId, string? Candidate_Status, string? Company_Status)
        {
            var oldData = await GetApplicationModelById(applicationId);

            //oldData = null;
            if (oldData == null)
            {
                return await Task.FromResult(false);
            }

            if (!string.IsNullOrEmpty(Candidate_Status))
            {
                oldData!.Candidate_Status = Candidate_Status.ToString();
            }

            if (!string.IsNullOrEmpty(Company_Status))
            {
                oldData!.Company_Status = Company_Status.ToString();
            }

            #region old status

            //if (data.Company_Status == "Pending")
            //{
            //    if (newStatus.Equals("Rejected"))
            //    {
            //        // Do nothing
            //    }
            //    else if (newStatus.Equals("Accepted"))
            //    {
            //        data.Candidate_Status = "Passed";
            //    }
            //    else if (newStatus.Equals("Pending"))
            //    {
            //        //It's default. Do nothing
            //    }
            //}
            //else if (newStatus.Equals("Accepted"))
            //{
            //    // Do nothing
            //}
            //else
            //{
            //    // newStatus == "Rejected"
            //    //Do nothing
            //}

            #endregion old status

            return await _applicationRepository.UpdateApplication(oldData, applicationId);
        }

        public async Task<ApplicationModel?> GetApplicationModelById(Guid applicationId)
        {
            return await _applicationRepository.GetApplicationById(applicationId);
        }
    }
}