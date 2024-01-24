using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Shift
{
    public Guid ShiftId { get; set; }

    public int ShiftTimeStart { get; set; }

    public int ShiftTimeEnd { get; set; }

    public virtual ICollection<Itrsinterview> Itrsinterviews { get; set; } = new List<Itrsinterview>();
}
