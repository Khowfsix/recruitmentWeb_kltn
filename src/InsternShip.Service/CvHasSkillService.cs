using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.CvHasSkill;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class CvHasSkillService : ICvHasSkillService
    {
        private readonly ICvHasSkillrepository _cvHasSkillrepository;
        private readonly IMapper _mapper;

        public CvHasSkillService(ICvHasSkillrepository cvHasSkillrepository, IMapper mapper)
        {
            _cvHasSkillrepository = cvHasSkillrepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCvHasSkillService(Guid requestId)
        {
            return await _cvHasSkillrepository.DeleteCvHasSkillService(requestId);
        }

        public async Task<IEnumerable<CvHasSkillViewModel>> GetAllCvHasSkillService(string? request)
        {
            var data = await _cvHasSkillrepository.GetAllCvHasSkillService(request);
            List<CvHasSkillViewModel> cvHasSkillViewModels = new List<CvHasSkillViewModel>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var obj = _mapper.Map<CvHasSkillViewModel>(item);
                    cvHasSkillViewModels.Add(obj);
                }
                return cvHasSkillViewModels;
            }
            return null;
        }

        public async Task<CvHasSkillViewModel> SaveCvHasSkillService(CvHasSkillAddModel request)
        {
            var data = _mapper.Map<CvHasSkillModel>(request);
            var response = await _cvHasSkillrepository.SaveCvHasSkillService(data);

            return _mapper.Map<CvHasSkillViewModel>(response);
        }

        public async Task<bool> UpdateCvHasSkillService(CvHasSkillUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<CvHasSkillModel>(request);
            return await _cvHasSkillrepository.UpdateCvHasSkillService(data, requestId);
        }
    }
}