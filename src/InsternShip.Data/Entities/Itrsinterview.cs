using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Itrsinterview
{
    public Guid ItrsinterviewId { get; set; }

    public DateTime DateInterview { get; set; }

    public Guid ShiftId { get; set; }

    public Guid RoomId { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual Room Room { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
