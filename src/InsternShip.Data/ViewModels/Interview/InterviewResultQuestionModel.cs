using InsternShip.Data.ViewModels.Round;

namespace InsternShip.Data.ViewModels.Interview
{
    public class InterviewResultQuestionModel
    {
        public Guid InterviewId { get; set; }
        public string? Notes { get; set; }
        public ICollection<RoundResultAddModel> Rounds { get; set; } = new List<RoundResultAddModel>();
    }
}