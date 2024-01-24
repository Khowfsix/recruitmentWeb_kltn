using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Requirement;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _reportRepository;
        private readonly IMapper _mapper;

        public RequirementService(IRequirementRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<RequirementViewModel> SaveRequirement(RequirementAddModel reportModel)
        {
            var data = _mapper.Map<RequirementModel>(reportModel);
            var response = await _reportRepository.SaveRequirement(data);
            return _mapper.Map<RequirementViewModel>(response);
        }

        public async Task<bool> DeleteRequirement(Guid reportModelId)
        {
            return await _reportRepository.DeleteRequirement(reportModelId);
        }

        public async Task<IEnumerable<RequirementViewModel>> GetAllRequirement()
        {
            var modelDatas = await _reportRepository.GetAllRequirement();
            List<RequirementViewModel> list = new List<RequirementViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<RequirementViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateRequirement(RequirementUpdateModel reportModel, Guid reportModelId)
        {
            var data = _mapper.Map<RequirementModel>(reportModel);
            return await _reportRepository.UpdateRequirement(data, reportModelId);
        }
    }
}