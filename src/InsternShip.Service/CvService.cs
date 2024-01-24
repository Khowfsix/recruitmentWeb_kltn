using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Data.ViewModels.Cv;
using InsternShip.Data.ViewModels.Skill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InsternShip.Service
{
    public class CvService : ICvService
    {
        private readonly ICvRepository _cvRepository;
        private readonly ICvHasSkillrepository _cvHasSkillRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public CvService(
            ICvRepository cvRepository,
            ICvHasSkillrepository cvHasSkillRepository,
            ICandidateRepository candidateRepository,
            ICertificateRepository certificateRepository,
            IMapper mapper, IUploadFileService uploadFileService)
        {
            _cvRepository = cvRepository;
            _cvHasSkillRepository = cvHasSkillRepository;
            _candidateRepository = candidateRepository;
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
        }

        public async Task<bool> DeleteCv(Guid requestId)
        {
            return await _cvRepository.DeleteCv(requestId);
        }

        public async Task<IEnumerable<CvViewModel>> GetAllCv(string? request)
        {
            // Get Cv thuộc về candidate
            var data = await _cvRepository.GetAllCv(request);

            var resp = _mapper.Map<IList<CvViewModel>>(data);

            foreach (var item in resp)
            {
                // Tìm skills và gắn vào
                var skills = await _cvHasSkillRepository.GetSkill(item.Cvid);

                var skillVMs = _mapper.Map<IList<SkillViewModel>>(skills);

                item.Skills = skillVMs;

                // Tìm certificates và gắn vào
                var certificates = await _certificateRepository.GetForeignKey(item.Cvid);

                var certificateVMs = _mapper.Map<IList<CertificateViewModel>>(certificates);

                item.Certificates = certificateVMs;
            }

            return resp;
        }

        public async Task<CvViewModel> SaveCv(CvAddModel request)
        {
            // Get list skill
            var skills = request.Skills;

            // Get list Certificate
            var CertificateVMs = request.Certificates;

            var data = _mapper.Map<CvModel>(request);

            // Create cv
            var result = await _cvRepository.SaveCv(data);

            if (result.Item1 == false)
                return null!;

            var cv = result.Item2;

            // Create Cv skill
            foreach (var skill in skills)
            {
                var cvHasSkill = new CvHasSkillModel()
                {
                    Cvid = cv.Cvid,
                    SkillId = skill.SkillId,
                    ExperienceYear = skill.ExperienceYear
                };

                var cvSkillResp = await _cvHasSkillRepository.SaveCvHasSkillService(cvHasSkill);

                if (cvSkillResp == null)
                {
                    throw new Exception("Fail to create CvHasSkill");
                }
            }

            // Create Cv Certificate
            foreach (var certificateVM in CertificateVMs)
            {
                certificateVM.Cvid = cv.Cvid;
                var certificate = _mapper.Map<CertificateModel>(certificateVM);

                var certResp = await _certificateRepository.SaveCertificate(certificate);

                if (certResp == null)
                {
                    throw new Exception("Failed to save certificate");
                }
            }
            var cvVM = GetCvById(result.Item2.Cvid);
            return await cvVM;
        }

        public async Task<bool> UpdateCv(CvUpdateModel request, Guid requestId)
        {
            // Get list skill
            var skills = request.Skills;

            // Get list Certificate
            var CertificateVMs = request.Certificates;

            // Update Cv
            var data = _mapper.Map<CvModel>(request);
            var result = await _cvRepository.UpdateCv(data, requestId);

            if (result == false)
                throw new Exception("Cannot update CV");

            // Update Cv Has Skill
            var listCvHasSkill = await _cvHasSkillRepository.GetAllSkillsFromOneCV(requestId);

            //// Delete all
            foreach (var cvHasSkill in listCvHasSkill)
            {
                await _cvHasSkillRepository.DeleteCvHasSkillService(cvHasSkill.CvSkillsId);
            }

            //// Create new
            foreach (var skill in skills)
            {
                var cvHasSkill = new CvHasSkillModel()
                {
                    Cvid = requestId,
                    SkillId = skill.SkillId,
                    ExperienceYear = skill.ExperienceYear
                };

                await _cvHasSkillRepository.SaveCvHasSkillService(cvHasSkill);
            }

            // Update Certificate
            var certificates = await _certificateRepository.GetForeignKey(requestId);

            //// Delete all
            foreach (var certificate in certificates)
            {
                await _certificateRepository.DeleteCertificate(certificate.CertificateId);
            }

            //// Create new
            foreach (var certificateVM in CertificateVMs)
            {
                var certificate = _mapper.Map<CertificateModel>(certificateVM);

                await _certificateRepository.SaveCertificate(certificate);
            }

            return true;
        }

        public async Task<CvViewModel> GetCvById(Guid requestId)
        {
            // Get cv from cv id
            var data = await _cvRepository.GetCVById(requestId);

            // Get skill from cv id
            var skills = await _cvHasSkillRepository.GetSkill(requestId);

            var skillVMs = _mapper.Map<IList<SkillViewModel>>(skills);

            // Gắn skill vào cv
            var resp = _mapper.Map<CvViewModel>(data);
            resp.Skills = skillVMs;

            // Tìm certificates và gắn vào
            var certificates = await _certificateRepository.GetForeignKey(requestId);

            var certificateVMs = _mapper.Map<IList<CertificateViewModel>>(certificates);

            resp.Certificates = certificateVMs;
            return resp;
        }

        public async Task<IEnumerable<CvViewModel>> GetCvsOfCandidate(Guid candidateId)
        {
            // Get Cv thuộc về candidate
            var data = await _cvRepository.GetForeignKey(candidateId);

            var resp = _mapper.Map<IEnumerable<CvViewModel>>(data);

            // Tìm skills và gắn vào
            foreach (var item in resp)
            {
                // Tìm skills và gắn vào
                var skills = await _cvHasSkillRepository.GetSkill(item.Cvid);

                var skillVMs = _mapper.Map<IList<SkillViewModel>>(skills);

                item.Skills = skillVMs;

                // Tìm certificates và gắn vào
                var certificates = await _certificateRepository.GetForeignKey(item.Cvid);

                var certificateVMs = _mapper.Map<IList<CertificateViewModel>>(certificates);

                item.Certificates = certificateVMs;
            }

            return resp;
        }

        public async Task<IEnumerable<CvViewModel>> GetAllUserCv(string userId)
        {
            var data = await _cvRepository.GetAllUserCv(userId);
            var resp = _mapper.Map<IEnumerable<CvViewModel>>(data);
            return resp;
        }

        public async Task<bool> UploadCvPdf(IFormFile? CvFile, Guid Cvid)
        {
            // Upload file to Cloud
            if (CvFile == null)
            {
                return false;
            }

            var resp = await _uploadFileService.AddFileAsync(CvFile);
            var CvLink = resp.Url != null ? resp.Url.ToString() : "";

            var cvModel = await _cvRepository.GetCVById(Cvid);

            if (cvModel == null)
            {
                throw new Exception("CV not found");
            }

            cvModel.CvPdf = CvLink;

            var updated = await _cvRepository.UpdateCv(cvModel, cvModel.Cvid);

            return updated;
        }
    }
}