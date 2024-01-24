using InsternShip.Data.Entities;

namespace InsternShip.Data.Models
{
    public class CandidateModel
    {
        public Guid CandidateId { get; set; }

        public string UserId { get; set; }

        public virtual WebUser User { get; set; }

        public string? Experience { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}