using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Data.ViewModels.Skill;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Cv
{
    public class CvUpdateModel
    {
        public Guid Cvid { get; set; }
        public Guid CandidateId { get; set; }
        public string? Experience { get; set; }
        public string? CvPdf { get; set; }
        public string CvName { get; set; }
        public string Introduction { get; set; }
        public string Education { get; set; }
        public bool IsDeleted { get; set; }
        public IList<CvSkillRelationUpdateModel> Skills { get; set; } = new List<CvSkillRelationUpdateModel>();
        public IList<CertificateUpdateModel> Certificates { get; set; } = new List<CertificateUpdateModel>();
    }

    public class CvSkillRelationUpdateModel
    {
        public Guid SkillId { get; set; }
        public int ExperienceYear { get; set; }
    }
}
