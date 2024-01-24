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
    public class SecurityAnswer
    {
        [Key]
        public Guid SecurityAnswerId;

        [Required]
        public string AnswerString = null!;

        public Guid SecurityQuestionId;

        public string WebUserId;

        public virtual SecurityQuestion SecurityQuestion { get; set; }
        public virtual WebUser WebUser { get; set; }
    }
}
