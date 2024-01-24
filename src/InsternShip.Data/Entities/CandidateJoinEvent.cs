using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class CandidateJoinEvent
{
    public Guid CandidateJoinEventId { get; set; }

    public Guid CandidateId { get; set; }

    public Guid EventId { get; set; }

    public DateTime DateJoin { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}