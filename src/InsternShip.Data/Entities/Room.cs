using System;
using System.Collections.Generic;

namespace InsternShip.Data.Entities;

public partial class Room
{
    public Guid RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public bool? IsDeleted { get; set; } = null!;

    public virtual ICollection<Itrsinterview> Itrsinterviews { get; set; } = new List<Itrsinterview>();
}
