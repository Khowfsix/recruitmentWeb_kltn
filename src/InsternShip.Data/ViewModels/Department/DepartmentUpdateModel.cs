using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.ViewModels.Department
{
    public class DepartmentUpdateModel
    {
        [Key]
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string? Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [DataType(DataType.Url)]
        public string? Website { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}