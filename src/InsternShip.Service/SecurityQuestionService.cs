using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.SecurityQuestion;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private readonly ISecurityQuestionRepository _securityQuestionRepository;
        private readonly IMapper _mapper;

        public SecurityQuestionService(ISecurityQuestionRepository securityQuestionRepository, IMapper mapper)
        {
            _securityQuestionRepository = securityQuestionRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteSecurityQuestion(Guid requestId)
        {
            return await _securityQuestionRepository.RemoveSecurityQuestion(requestId);
        }

        public async Task<IEnumerable<SecurityQuestionViewModel>> GetAllSecurityQuestion()
        {
            var modelDatas = await _securityQuestionRepository.GetSecurityQuestion();
            List<SecurityQuestionViewModel> list = new List<SecurityQuestionViewModel>();
            foreach (var item in modelDatas)
            {
                list.Add(_mapper.Map<SecurityQuestionViewModel>(item));
            }
            return list;
        }

        public async Task<SecurityQuestionViewModel> SaveSecurityQuestion(SecurityQuestionAddModel request)
        {
            var data = _mapper.Map<SecurityQuestionModel>(request);
            var response = await _securityQuestionRepository.AddSecurityQuestion(data);
            return _mapper.Map<SecurityQuestionViewModel>(response);
        }

        public Task<bool> UpdateSecurityQuestion(SecurityQuestionUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<SecurityQuestionModel>(request);
            return _securityQuestionRepository.UpdateSecurityQuestion(data, requestId);
        }
    }
}