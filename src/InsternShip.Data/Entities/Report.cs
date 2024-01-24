using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Report
{
    public Guid ReportId { get; set; }

    public string? ReportName { get; set; }

    public Guid RecruiterId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Recruiter Recruiter { get; set; } = null!;
}
