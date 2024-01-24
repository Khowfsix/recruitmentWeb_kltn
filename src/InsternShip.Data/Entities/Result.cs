namespace InsternShip.Data.Entities;

public partial class Result
{
    public Guid ResultId { get; set; }

    public string? ResultString { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}