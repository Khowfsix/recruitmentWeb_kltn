using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Requirement
{
    public Guid RequirementId { get; set; }

    public Guid PositionId { get; set; }

    public Guid SkillId { get; set; }

    public string Experience { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
