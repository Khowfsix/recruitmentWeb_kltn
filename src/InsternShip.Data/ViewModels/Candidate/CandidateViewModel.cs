using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Candidate
{
    public class CandidateViewModel
    {
        public Guid CandidateId { get; set; }
        public string UserId { get; set; }
        public string? Experience { get; set; }
        public bool IsDeleted { get; set; } = false;
        public WebUserViewModel User { get; set; }
    }
}
