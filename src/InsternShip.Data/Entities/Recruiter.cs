using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Recruiter
{
    public Guid RecruiterId { get; set; }

    public string UserId { get; set; }

    public Guid DepartmentId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual WebUser User { get; set; } = null!;
}
