namespace InsternShip.Data.ViewModels.Round
{
    public class RoundAddModel
    {
        public Guid InterviewId { get; set; }

        public Guid QuestionId { get; set; }

        public double? Score { get; set; }
    }
}