using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class BlacklistModel
    {
        public Guid BlackListId { get; set; }
        public Guid CandidateId { get; set; }
        public string? Reason { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int Status { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}