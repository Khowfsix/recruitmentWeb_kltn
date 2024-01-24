using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class CategoryQuestion
{
    public Guid CategoryQuestionId { get; set; }

    public string? CategoryQuestionName { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
