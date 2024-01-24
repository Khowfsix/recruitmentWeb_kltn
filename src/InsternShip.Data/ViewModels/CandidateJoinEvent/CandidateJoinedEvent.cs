using InsternShip.Data.ViewModels.Event;
using InsternShip.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.CandidateJoinEvent
{
    public class CandidateJoinedEvent
    {

        public Guid CandidateId { get; set; }

        public Guid CandidateJoinEventId { get; set; }

        public Guid EventId { get; set; }
        
        public EventViewModel Event { get; set; }

    }
}
