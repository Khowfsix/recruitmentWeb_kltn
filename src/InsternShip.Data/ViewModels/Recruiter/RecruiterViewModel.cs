namespace InsternShip.Data.ViewModels.Recruiter
{
    public class RecruiterViewModel
    {
        public Guid RecruiterId { get; set; }
        public string UserId { get; set; }

        public Guid DepartmentId { get; set; }
        public WebUserViewModel User { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
