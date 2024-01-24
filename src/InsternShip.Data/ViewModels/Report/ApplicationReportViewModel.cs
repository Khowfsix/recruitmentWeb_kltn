using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Report
{
    public class ApplicationReportViewModel
    {
        public Guid ApplicationId { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string? Experience { get; set; }
        public string CvName { get; set; } = null!;
        public string Introduction { get; set; } = null!;
        public string Education { get; set; } = null!;
        public string? PositionName { get; set; }
        public string? Description { get; set; }
        public decimal? Salary { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string LanguageName { get; set; } = null!;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Candidate_Status { get; set; } = "Pending";
        public string Company_Status { get; set; } = "Pending";
        public string? Priority { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
