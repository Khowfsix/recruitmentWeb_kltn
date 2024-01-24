using Microsoft.AspNetCore.Identity;

namespace InsternShip.Data.Entities;

public partial class WebUser : IdentityUser
{
    public string? FullName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = null!;

    public string? ImageURL { get; set; } = null;

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<Interviewer> Interviewers { get; set; } = new List<Interviewer>();

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();

    public virtual ICollection<SecurityAnswer> SecurityAnswers { get; set; } = new List<SecurityAnswer>();
}