namespace InsternShip.Data.ViewModels.Itrsinterview;

public class ItrsinterviewUpdateModel
{
    public Guid ItrsinterviewId { get; set; }
    public DateTime DateInterview { get; set; }

    public Guid ShiftId { get; set; }

    public Guid RoomId { get; set; }
}