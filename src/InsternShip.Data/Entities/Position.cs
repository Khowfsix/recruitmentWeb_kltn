using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Position
{
    public Guid PositionId { get; set; }

    public string? PositionName { get; set; }

    public string? Description { get; set; }

    public string? ImageURL { get; set; }

    public decimal? Salary { get; set; }

    public int MaxHiringQty { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid DepartmentId { get; set; }

    public Guid LanguageId { get; set; }

    public Guid RecruiterId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Department Department { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;

    public virtual Recruiter Recruiter { get; set; } = null!;

    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();

    public virtual ICollection<SuccessfulCadidate> SuccessfulCadidates { get; set; } = new List<SuccessfulCadidate>();
}
