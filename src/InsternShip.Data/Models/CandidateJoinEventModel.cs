using InsternShip.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class CandidateJoinEventModel
    {
        public Guid CandidateJoinEventId { get; set; }

        public Guid CandidateId { get; set; }

        public Guid EventId { get; set; }

        public DateTime? DateJoin { get; set; } = DateTime.Now;
        public int JoinEventCount { get; set; }

        public EventModel Event { get; set; }
        public CandidateModel Candidate { get; set; }
    }
}
