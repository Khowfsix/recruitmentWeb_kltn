using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.CvHasSkill
{
    public class CvHasSkillViewModel
    {
        public Guid CvSkillsId { get; set; }
        public Guid Cvid { get; set; }

        public Guid SkillId { get; set; }

        public int? ExperienceYear { get; set; }
    }
}
