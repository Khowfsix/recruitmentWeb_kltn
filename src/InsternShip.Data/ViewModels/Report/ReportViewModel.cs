using InsternShip.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Report
{
    public class ReportUpdateModel
    {
        public Guid ReportId { get; set; }
        public string? ReportName { get; set; }

        public Guid RecruiterId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
