using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class BlackList
{
    public Guid BlackListId { get; set; }

    public Guid CandidateId { get; set; }

    public string? Reason { get; set; }

    public DateTime DateTime { get; set; }

    public int? Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;
}
