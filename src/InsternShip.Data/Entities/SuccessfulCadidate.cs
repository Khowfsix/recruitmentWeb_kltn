using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class SuccessfulCadidate
{
    public Guid SuccessfulCadidateId { get; set; }

    public Guid PositionId { get; set; }

    public Guid CandidateId { get; set; }

    public DateTime DateSuccess { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
