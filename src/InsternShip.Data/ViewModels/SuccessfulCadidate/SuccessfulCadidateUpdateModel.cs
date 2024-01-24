namespace InsternShip.Data.ViewModels.SuccessfulCadidate
{
    public class SuccessfulCadidateUpdateModel
    {
        public Guid SuccessfulCadidateId { get; set; }
        public Guid PositionId { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime DateSuccess { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}