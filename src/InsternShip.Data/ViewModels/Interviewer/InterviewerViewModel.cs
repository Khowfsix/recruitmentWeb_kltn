namespace InsternShip.Data.ViewModels.Interviewer
{
    public class InterviewerViewModel
    {
        public Guid InterviewerId { get; set; }

        public string UserId { get; set; }

        public Guid DepartmentId { get; set; }
        public WebUserViewModel User { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
