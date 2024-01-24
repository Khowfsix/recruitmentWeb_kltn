using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class QuestionSkill
{
    public Guid QuestionSkillsId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid SkillId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
