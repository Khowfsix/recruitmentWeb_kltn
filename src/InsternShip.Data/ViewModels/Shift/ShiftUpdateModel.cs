namespace InsternShip.Data.ViewModels.Shift
{
    public class ShiftUpdateModel
    {
        public Guid ShiftId { get; set; }
        public int ShiftTimeStart { get; set; }

        public int ShiftTimeEnd { get; set; }
    }
}