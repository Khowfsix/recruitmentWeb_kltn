using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models{
    public class CategoryQuestionModel
    {
        public Guid CategoryQuestionId { get; set; }

        public string? CategoryQuestionName { get; set; }

        public double Weight { get; set; }
    }
}



