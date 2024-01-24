namespace InsternShip.Data.Models
{
    public class ApplicationModel
    {
        public Guid ApplicationId { get; set; }
        public Guid Cvid { get; set; }
        public Guid PositionId { get; set; }
        public virtual CvModel Cv { get; set; } = null!;
        public virtual PositionModel Position { get; set; } = null!;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Candidate_Status { get; set; } = "Pending";
        public string Company_Status { get; set; } = "Pending";
        public string? Priority { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
