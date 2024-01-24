using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsternShip.Data.ViewModels.CandidateJoinEvent
{
    public class CandidateJoinEventUpdateModel
    {
        public string EventName { get; set; }
        public Guid CandidateJoinEventId { get; set; }

        public Guid CandidateId { get; set; }

        public Guid EventId { get; set; }


    }
}
