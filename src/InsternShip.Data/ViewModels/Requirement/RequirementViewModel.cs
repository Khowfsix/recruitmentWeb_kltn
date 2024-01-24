using InsternShip.Data.ViewModels.Position;
using InsternShip.Data.ViewModels.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Requirement
{
    public class RequirementViewModel
    {
        public Guid RequirementId { get; set; }
        public Guid PositionId { get; set; }
        public Guid SkillId { get; set; }

        public SkillViewModel Skill { get; set; }
        public PositionViewModel Position { get; set; }

        public string Experience { get; set; } = null!;

        public string? Notes { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
