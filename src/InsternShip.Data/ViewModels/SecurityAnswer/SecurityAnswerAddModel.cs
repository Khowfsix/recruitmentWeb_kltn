
namespace InsternShip.Data.ViewModels.SecurityAnswer
{
    public class SecurityAnswerAddModel
    {
        public string AnswerString { get; set; } = null!;

        public Guid SecurityQuestionId { get; set; }

        public string WebUserId { get; set; }
    }
}

