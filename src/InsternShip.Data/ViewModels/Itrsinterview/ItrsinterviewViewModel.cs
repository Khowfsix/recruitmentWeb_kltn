using InsternShip.Data.ViewModels.Room;
using InsternShip.Data.ViewModels.Shift;

namespace InsternShip.Data.ViewModels.Itrsinterview;

public class ItrsinterviewViewModel
{
    public Guid ItrsinterviewId { get; set; }
    public DateTime DateInterview { get; set; }

    //public Guid ShiftId { get; set; }

    //public Guid RoomId { get; set; }

    public virtual RoomViewModel Room { get; set; } = null!;

    public virtual ShiftViewModel Shift { get; set; } = null!;
}