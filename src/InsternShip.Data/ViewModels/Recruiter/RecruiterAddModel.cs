namespace InsternShip.Data.ViewModels.Recruiter
{
    public class RecruiterAddModel
    {
        public string UserId { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
