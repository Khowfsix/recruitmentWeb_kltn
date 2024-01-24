namespace InsternShip.Data.ViewModels.Round
{
    public class RoundUpdateModel
    {
        public Guid RoundId { get; set; }
        public Guid InterviewId { get; set; }

        public Guid QuestionId { get; set; }

        public double? Score { get; set; }
    }
}