namespace InsternShip.Data.Models
{
    public class SuccessfulCadidateModel
    {
        public Guid SuccessfulCadidateId { get; set; }

        public Guid PositionId { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime DateSuccess { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}