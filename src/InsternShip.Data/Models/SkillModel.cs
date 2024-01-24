namespace InsternShip.Data.Models
{
    public class SkillModel
    {
        public Guid SkillId { get; set; }

        public string SkillName { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}