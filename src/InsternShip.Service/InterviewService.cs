using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Interview;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service;

public class InterviewService : IInterviewService
{
    private readonly IInterviewRepository _interviewRepository;
    private readonly IItrsinterviewRepository _itrsinterviewRepository;
    private readonly IRoundRepository _roundRepository;
    private readonly IMapper _mapper;

    public InterviewService(IInterviewRepository interviewRepository,
        IRoundRepository roundRepository,
        IItrsinterviewRepository itrsinterviewRepository,
        IMapper mapper)
    {
        _interviewRepository = interviewRepository;
        _itrsinterviewRepository = itrsinterviewRepository;
        _roundRepository = roundRepository;
        _mapper = mapper;
    }

    public async Task<InterviewViewModel> GetInterviewById(Guid id)
    {
        var data = await _interviewRepository.GetInterviewById(id);
        var result = _mapper.Map<InterviewViewModel>(data);
        return result;
    }

    public async Task<InterviewViewModel> SaveInterview(InterviewAddModel interviewModel)
    {
        var interviewData = _mapper.Map<InterviewModel>(interviewModel);

        var response = await _interviewRepository.SaveInterview(interviewData);
        return _mapper.Map<InterviewViewModel>(response);
    }

    public async Task<bool> DeleteInterview(Guid interviewModelId)
    {
        var thisInterview = await GetInterviewById_noInclude(interviewModelId);

        var deleteITRS = await _itrsinterviewRepository.DeleteItrsinterview((Guid)thisInterview.ItrsinterviewId);

        var response = await _interviewRepository.DeleteInterview(interviewModelId);
        return response;
    }

    public async Task<bool> UpdateInterview(InterviewUpdateModel interviewModel, Guid interviewModelId)
    {
        var data = _mapper.Map<InterviewModel>(interviewModel);
        return await _interviewRepository.UpdateInterview(data, interviewModelId);
    }

    public async Task<IEnumerable<InterviewViewModel>> GetInterviewsByPositon(Guid requestId)
    {
        var data = await _interviewRepository.GetAllInterview();
        if (data != null)
        {
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            foreach (var item in data)
            {
                if (item.Application.Position.PositionId.Equals(requestId))
                {
                    var obj = _mapper.Map<InterviewViewModel>(item);
                    result.Add(obj);
                }
            }
            return result;
        }
        return null!;
    }

    public async Task<IEnumerable<InterviewViewModel>> GetInterviewsByDepartment(Guid requestId)
    {
        var data = await _interviewRepository.GetAllInterview();
        if (data != null)
        {
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            foreach (var item in data)
            {
                if (item.Application.Position.Department.DepartmentId.Equals(requestId))
                {
                    var obj = _mapper.Map<InterviewViewModel>(item);
                    result.Add(obj);
                }
            }
            return result;
        }
        return null!;
    }

    public async Task<IEnumerable<InterviewViewModel>> GetAllInterview(string status)
    {
        var data = await _interviewRepository.GetAllInterview();
        if (data != null)
        {
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            foreach (var item in data)
            {
                if (item.Company_Status!.Contains(status) || item.Candidate_Status!.Contains(status))
                {
                    var obj = _mapper.Map<InterviewViewModel>(item);
                    result.Add(obj);
                }
            }
            return result;
        }
        return null!;
    }

    public async Task<IEnumerable<InterviewViewModel>> GetInterviewsByInterviewer(Guid requestId)
    {
        var data = await _interviewRepository.GetAllInterview();
        if (data != null)
        {
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            foreach (var item in data)
            {
                if (item.InterviewerId.Equals(requestId))
                {
                    var obj = _mapper.Map<InterviewViewModel>(item);
                    result.Add(obj);
                }
            }
            return result;
        }
        return null!;
    }

    public async Task<bool> UpdateStatusInterview(Guid interviewId, string? Candidate_Status, string? Company_Status)
    {
        var oldData = await GetInterviewById_noInclude(interviewId);

        if (!string.IsNullOrEmpty(Candidate_Status))
        {
            oldData!.Candidate_Status = Candidate_Status!;
        }

        if (!string.IsNullOrEmpty(Company_Status))
        {
            oldData!.Company_Status = Company_Status!;
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
        //        data.Candidate_Status = "Passing";
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

        return await _interviewRepository.UpdateInterview(oldData!, interviewId);
    }

    public async Task<InterviewModel?> GetInterviewById_noInclude(Guid id)
    {
        return await _interviewRepository.GetInterviewById_NoInclude(id);
    }

    public async Task<InterviewModel?> PostQuestionIntoInterview(InterviewResultQuestionModel request)
    {
        var thisInterview = await GetInterviewById_noInclude(request.InterviewId);

        if (thisInterview == null)
        {
            return null!;
        }
        thisInterview.Notes = request.Notes;

        var updateInterview = await _interviewRepository.UpdateInterview(thisInterview, request.InterviewId);

        foreach (var item in request.Rounds)
        {
            var newRound = new RoundModel()
            {
                RoundId = Guid.Empty,
                InterviewId = request.InterviewId,
                QuestionId = item.QuestionId,
                Score = item.Score,
            };

            var respInsertRounds = await _roundRepository.SaveRound(newRound);

            if (respInsertRounds == null)
            {
                return null!;
            }
        }

        if (updateInterview != true)
        {
            return null!;
        }

        return thisInterview;
    }
}