using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.Entities;

public partial class Department
{
    public Guid DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Address { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [DataType(DataType.Url)]
    public string? Website { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Interviewer> Interviewers { get; set; } = new List<Interviewer>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
}
