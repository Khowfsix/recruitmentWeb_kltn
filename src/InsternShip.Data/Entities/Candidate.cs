namespace InsternShip.Data.Entities;

public partial class Candidate
{
    public Guid CandidateId { get; set; }

    public string UserId { get; set; }

    public string? Experience { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<BlackList> BlackLists { get; set; } = new List<BlackList>();

    public virtual ICollection<CandidateJoinEvent> CandidateJoinEvents { get; set; } = new List<CandidateJoinEvent>();

    public virtual ICollection<Cv> Cvs { get; set; } = new List<Cv>();

    public virtual ICollection<SuccessfulCadidate> SuccessfulCadidates { get; set; } = new List<SuccessfulCadidate>();

    public virtual WebUser User { get; set; }
}