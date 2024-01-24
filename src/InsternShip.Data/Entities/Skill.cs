using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Skill
{
    public Guid SkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<CvHasSkill> CvHasSkills { get; set; } = new List<CvHasSkill>();

    public virtual ICollection<QuestionSkill> QuestionSkills { get; set; } = new List<QuestionSkill>();

    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
}
