namespace InsternShip.Data.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}