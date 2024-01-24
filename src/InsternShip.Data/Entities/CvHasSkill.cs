using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class CvHasSkill
{
    public Guid CvSkillsId { get; set; }

    public Guid Cvid { get; set; }

    public Guid SkillId { get; set; }

    public int? ExperienceYear { get; set; }

    public virtual Cv Cv { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
