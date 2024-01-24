using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Entities
{
    public class SecurityAnswerModel
    {
        public Guid SecurityAnswerId { get; set; }

        public string AnswerString { get; set; } = null!;

        public Guid SecurityQuestionId { get; set; }

        public string WebUserId { get; set; }
    }
}
