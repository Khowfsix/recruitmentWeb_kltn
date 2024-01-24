using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Event
{
    public class EventAddModel
    {
        public string EventName { get; set; } = null!;

        public Guid RecruiterId { get; set; }

        public string? Description { get; set; }

        public string Place { get; set; } = null!;

        public DateTime? DatetimeEvent { get; set; }

        public int MaxParticipants { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
