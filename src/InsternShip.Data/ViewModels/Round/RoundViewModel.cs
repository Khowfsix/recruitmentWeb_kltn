using InsternShip.Data.ViewModels.Question;

namespace InsternShip.Data.ViewModels.Round
{
    public class RoundViewModel
    {
        public Guid RoundId { get; set; }
        public Guid InterviewId { get; set; }
        public Guid QuestionId { get; set; }
        public virtual QuestionViewModel Question { get; set; } = null!;
        public double? Score { get; set; }
    }
}