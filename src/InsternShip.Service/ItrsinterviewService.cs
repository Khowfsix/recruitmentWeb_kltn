using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service;

public class ItrsinterviewService : IItrsinterviewService
{
    private readonly IItrsinterviewRepository _itrsinterviewRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IMapper _mapper;

    public ItrsinterviewService(IItrsinterviewRepository itrsinterviewRepository, IInterviewRepository interviewRepository, IMapper mapper)
    {
        _itrsinterviewRepository = itrsinterviewRepository;
        _interviewRepository = interviewRepository;
        _mapper = mapper;
    }

    public async Task<ItrsinterviewViewModel?> SaveItrsinterview(ItrsinterviewAddModel itrsinterviewModel, Guid interviewerId)
    {
        // Nếu ca, phòng đó đã có người đặt
        if (await ExistITRS(itrsinterviewModel, interviewerId) == true)
        {
            return null!;
        }

        try
        {
            var data = _mapper.Map<ItrsinterviewModel>(itrsinterviewModel);
            var response = await _itrsinterviewRepository.SaveItrsinterview(data, interviewerId);

            if (response != null)
            {
                return _mapper.Map<ItrsinterviewViewModel>(response);
            }

            return null!;

        }
        catch (Exception)
        {
            return null!;
            throw;
        }
    }

    public async Task<bool> DeleteItrsinterview(Guid itrsinterviewModelId)
    {
        return await _itrsinterviewRepository.DeleteItrsinterview(itrsinterviewModelId);
    }

    public async Task<IEnumerable<ItrsinterviewViewModel>> GetAllItrsinterview()
    {
        var data = await _itrsinterviewRepository.GetAllItrsinterview();
        if (data != null)
        {
            List<ItrsinterviewViewModel> result = new List<ItrsinterviewViewModel>();
            foreach (var item in data)
            {
                var obj = _mapper.Map<ItrsinterviewViewModel>(item);
                result.Add(obj);
            }
            return result;
        }
        return null;
    }

    public async Task<bool> UpdateItrsinterview(ItrsinterviewUpdateModel itrsinterviewModel, Guid itrsinterviewId, Guid interviewerId)
    {
        var addData = new ItrsinterviewAddModel
        {
            DateInterview = itrsinterviewModel.DateInterview,
            RoomId = itrsinterviewModel.RoomId,
            ShiftId = itrsinterviewModel.ShiftId
        };

        if (await ExistITRS(addData, interviewerId) == true)
        {
            return false;
        }

        var data = _mapper.Map<ItrsinterviewModel>(itrsinterviewModel);
        return await _itrsinterviewRepository.UpdateItrsinterview(data, itrsinterviewId);
    }

    public async Task<ItrsinterviewViewModel?> GetItrsinterviewById(Guid id)
    {
        var data = await _itrsinterviewRepository.GetItrsinterviewById(id);
        var result = _mapper.Map<ItrsinterviewViewModel>(data);
        return result;
    }

    public async Task<bool> ExistITRS(ItrsinterviewAddModel itrsinterview, Guid interviewerId)
    {
        if (itrsinterview == null)
        {
            return await Task.FromResult(false);
        }

        // check ngày giờ phòng đó có ai đăng ký chưa
        var exists = await _itrsinterviewRepository.GetAllItrsinterview_NoInclude();
        foreach (var item in exists)
        {
            if ((item.DateInterview.Date.Equals(itrsinterview.DateInterview.Date)) &&
                (item.ShiftId.Equals(itrsinterview.ShiftId)) &&
                (item.RoomId.Equals(itrsinterview.RoomId)))
            {
                return await Task.FromResult(true);
            }
        }

        //check interviewer vào ngày giờ đó có itrs không
        var interviewOfInterviewer = await _interviewRepository.GetInterviewOfInterviewer(interviewerId);
        foreach (var item in interviewOfInterviewer)
        {
            if ((item.Itrsinterview.DateInterview.Date.Equals(itrsinterview.DateInterview.Date)) &&
                (item.Itrsinterview.ShiftId.Equals(itrsinterview.ShiftId)))
            {
                return await Task.FromResult(true);
            }
        }

        return await Task.FromResult(false);
    }
}