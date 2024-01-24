using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Entities
{
    public partial class ResetPassword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string OTP { get; set; }

        [Required]
        public DateTime InsertDateTimeUTC { get; set; }
    }
}
