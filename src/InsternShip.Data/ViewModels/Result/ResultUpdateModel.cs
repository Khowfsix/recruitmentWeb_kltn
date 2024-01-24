using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.Result
{
    public class ResultUpdateModel
    {
        public Guid ResultId { get; set; }
        public string? ResultString { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
