namespace InsternShip.Data.ViewModels.BlackList
{
    public class BlackListUpdateModel
    {
        public Guid BlackListId { get; set; }
        public Guid CandidateId { get; set; }
        public string? Reason { get; set; }
        public DateTime DateTime { get; set; }
        public int? Status { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}