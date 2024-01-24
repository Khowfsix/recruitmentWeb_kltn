using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.CategoryQuestion
{
    public class CategoryQuestionViewModel
    {
        public Guid CategoryQuestionId { get; set; }
        public string? CategoryQuestionName { get; set; }
        public double Weight { get; set; }
    }

}
