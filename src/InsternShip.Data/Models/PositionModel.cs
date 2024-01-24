using InsternShip.Data.Entities;
using System;
using System.Collections.Generic;

namespace InsternShip.Data.Models;

public class PositionModel
{
    public Guid PositionId { get; set; }

    public string? PositionName { get; set; }

    public string? Description { get; set; }

    public decimal? Salary { get; set; }

    public int MaxHiringQty { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid DepartmentId { get; set; }
    public DepartmentModel Department { get; set; }

    public Guid LanguageId { get; set; }
    public LanguageModel Language { get; set; }

    public Guid RecruiterId { get; set; }
    public RecruiterModel Recruiter { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<RequirementModel> Requirements { get; set; } = new List<RequirementModel>();
}
