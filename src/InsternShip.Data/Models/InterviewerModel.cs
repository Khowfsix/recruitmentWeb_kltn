using InsternShip.Data.Entities;

namespace InsternShip.Data.Models
{
    public class InterviewerModel
    {
        public Guid InterviewerId { get; set; }

        public string UserId { get; set; }

        public virtual WebUser User { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
