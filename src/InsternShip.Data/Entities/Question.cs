using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Question
{
    public Guid QuestionId { get; set; }

    public string QuestionString { get; set; } = null!;

    public Guid CategoryQuestionId { get; set; }

    public virtual CategoryQuestion CategoryQuestion { get; set; } = null!;

    public virtual ICollection<QuestionSkill> QuestionSkills { get; set; } = new List<QuestionSkill>();

    public virtual ICollection<QuestionLanguage> QuestionLanguages { get; set; } = new List<QuestionLanguage>();

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
