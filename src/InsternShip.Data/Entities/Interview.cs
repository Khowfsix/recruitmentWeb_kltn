namespace InsternShip.Data.Entities;

public partial class Interview
{
    public Guid InterviewId { get; set; }

    public Guid InterviewerId { get; set; }

    public Guid RecruiterId { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid? ItrsinterviewId { get; set; }

    public string? Company_Status { get; set; }

    public string? Candidate_Status { get; set; }

    public string? Priority { get; set; }

    public Guid? ResultId { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Interviewer Interviewer { get; set; } = null!;

    public virtual Itrsinterview? Itrsinterview { get; set; }

    public virtual Recruiter Recruiter { get; set; } = null!;

    public virtual Result Result { get; set; } = null!;

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}