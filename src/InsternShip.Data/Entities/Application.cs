using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Application
{
    public Guid ApplicationId { get; set; }

    public Guid Cvid { get; set; }

    public Guid PositionId { get; set; }

    public DateTime DateTime { get; set; }

    public string? Company_Status { get; set; }

    public string? Candidate_Status { get; set; }

    public string? Priority { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Cv Cv { get; set; } = null!;

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual Position Position { get; set; } = null!;
}
