using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class DepartmentModel
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
