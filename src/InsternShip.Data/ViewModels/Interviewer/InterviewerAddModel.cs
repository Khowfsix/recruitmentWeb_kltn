namespace InsternShip.Data.ViewModels.Interviewer
{
    public class InterviewerAddModel
    {
        public string UserId { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
