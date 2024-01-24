using InsternShip.Data.ViewModels.Certificate;
using Microsoft.AspNetCore.Http;

namespace InsternShip.Data.ViewModels.Cv
{
    public class CvAddModel
    {
        //public Guid Cvid { get; set; }
        public Guid CandidateId { get; set; }
        public string? Experience { get; set; }
        public string? CvPdf { get; set; } = null!;
        public string CvName { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public IList<CvSkillRelationAddModel> Skills { get; set; } = new List<CvSkillRelationAddModel>();
        public IList<CertificateAddModel> Certificates { get; set; } = new List<CertificateAddModel>();
    }

    public class CvSkillRelationAddModel
    {
        public Guid SkillId { get; set; }
        public int ExperienceYear { get; set; }
    }
}
