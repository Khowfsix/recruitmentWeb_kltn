using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Room
{
    public class RoomUpdateModel
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; } = null!;
    }
}
