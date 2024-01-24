using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Recruiter;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service;

public class RecruiterService : IRecruiterService
{
    private readonly IRecruiterRepository _recruiterRepository;
    private readonly IMapper _mapper;

    public RecruiterService(IRecruiterRepository recruiterRepository, IMapper mapper)
    {
        _recruiterRepository = recruiterRepository;
        _mapper = mapper;
    }

    public async Task<RecruiterViewModel> SaveRecruiter(RecruiterAddModel recruiterModel)
    {
        var data = _mapper.Map<RecruiterModel>(recruiterModel);
        var response = await _recruiterRepository.SaveRecruiter(data);
        return _mapper.Map<RecruiterViewModel>(response);
    }

    public async Task<bool> DeleteRecruiter(Guid recruiterModelId)
    {
        return await _recruiterRepository.DeleteRecruiter(recruiterModelId);
    }

    public async Task<IEnumerable<RecruiterViewModel>> GetAllRecruiter()
    {
        var modelDatas = await _recruiterRepository.GetAllRecruiter();
        List<RecruiterViewModel> list = new List<RecruiterViewModel>();
        foreach (var item in modelDatas)
        {
            list.Add(_mapper.Map<RecruiterViewModel>(item));
        }
        return list;
    }

    public async Task<bool> UpdateRecruiter(RecruiterUpdateModel recruiterModel, Guid recruiterModelId)
    {
        var data = _mapper.Map<RecruiterModel>(recruiterModel);
        return await _recruiterRepository.UpdateRecruiter(data, recruiterModelId);
    }

    async Task<RecruiterViewModel?> IRecruiterService.GetRecruiterById(Guid id)
    {
        var data = await _recruiterRepository.GetRecruiterById(id);
        return _mapper.Map<RecruiterViewModel>(data);
    }
}