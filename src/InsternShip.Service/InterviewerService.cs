using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Interviewer;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service;

public class InterviewerService : IInterviewerService
{
    private readonly IInterviewerRepository _interviewerRepository;
    private readonly IMapper _mapper;

    public InterviewerService(IInterviewerRepository interviewerRepository, IMapper mapper)
    {
        _interviewerRepository = interviewerRepository;
        _mapper = mapper;
    }

    public async Task<InterviewerViewModel> SaveInterviewer(InterviewerAddModel addModel)
    {
        var data = _mapper.Map<InterviewerModel>(addModel);
        var response = await _interviewerRepository.SaveInterviewer(data);
        return _mapper.Map<InterviewerViewModel>(response);
    }

    public async Task<bool> DeleteInterviewer(Guid interviewerModelId)
    {
        return await _interviewerRepository.DeleteInterviewer(interviewerModelId);
    }

    public async Task<IEnumerable<InterviewerViewModel>> GetAllInterviewer()
    {
        var data = await _interviewerRepository.GetAllInterviewer();
        return data.Select(item => _mapper.Map<InterviewerViewModel>(item)).ToList();
    }

    public async Task<IEnumerable<InterviewerViewModel>> getInterviewersInDepartment(Guid departmentId)
    {
        var modelDatas = await _interviewerRepository.GetAllInterviewer();

        if (modelDatas != null)
        {
            List<InterviewerViewModel> datas = new List<InterviewerViewModel>();
            foreach (var item in modelDatas)
            {
                if (item.DepartmentId.Equals(departmentId))
                {
                    var data = _mapper.Map<InterviewerViewModel>(item);
                    datas.Add(data);
                }
            }
            return _mapper.Map<List<InterviewerViewModel>>(datas);
        }
        return null;
    }

    public async Task<bool> UpdateInterviewer(InterviewerUpdateModel interviewerModel, Guid interviewerModelId)
    {
        var data = _mapper.Map<InterviewerModel>(interviewerModel);
        return await _interviewerRepository.UpdateInterviewer(data, interviewerModelId);
    }

    public async Task<InterviewerViewModel?> GetInterviewerById(Guid id)
    {
        var data = await _interviewerRepository.GetInterviewerById(id);
        var result = _mapper.Map<InterviewerViewModel>(data);
        return result;
    }
}