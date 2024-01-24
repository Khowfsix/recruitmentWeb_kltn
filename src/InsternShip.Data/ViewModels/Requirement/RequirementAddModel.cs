using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Requirement
{
    public class RequirementAddModel
    {
        public Guid PositionId { get; set; }

        public Guid SkillId { get; set; }

        public string Experience { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
