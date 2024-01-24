using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.ViewModels.SecurityAnswer;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class SecurityAnswerService : ISecurityAnswerService
    {
        private readonly ISecurityAnswerRepository _reportRepository;
        private readonly IMapper _mapper;

        public SecurityAnswerService(ISecurityAnswerRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<SecurityAnswerViewModel> SaveSecurityAnswer(SecurityAnswerAddModel reportModel)
        {
            var data = _mapper.Map<SecurityAnswerModel>(reportModel);
            var response = await _reportRepository.SaveSecurityAnswer(data);
            return _mapper.Map<SecurityAnswerViewModel>(response);
        }

        public async Task<bool> DeleteSecurityAnswer(Guid reportModelId)
        {
            return await _reportRepository.DeleteSecurityAnswer(reportModelId);
        }

        public async Task<IEnumerable<SecurityAnswerViewModel>> GetAllSecurityAnswers()
        {
            var modelDatas = await _reportRepository.GetAllSecurityAnswers();
            List<SecurityAnswerViewModel> list = new List<SecurityAnswerViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<SecurityAnswerViewModel>(item));
            }
            return list;
        }

        public async Task<bool> UpdateSecurityAnswer(SecurityAnswerUpdateModel reportModel, Guid reportModelId)
        {
            var data = _mapper.Map<SecurityAnswerModel>(reportModel);
            return await _reportRepository.UpdateSecurityAnswer(data, reportModelId);
        }
    }
}