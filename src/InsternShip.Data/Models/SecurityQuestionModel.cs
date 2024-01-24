using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class SecurityQuestionModel
    {
        public Guid SecurityQuestionId { get; set; }
        public string QuestionString { get; set; } = null!;
    }
}
