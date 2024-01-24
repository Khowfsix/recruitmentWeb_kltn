using InsternShip.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class CvModel
    {
        public Guid Cvid { get; set; }
        public Guid CandidateId { get; set; }
        public virtual CandidateModel Candidate { get; set; } = null!;
        public string? Experience { get; set; }
        public string? CvPdf { get; set; }
        public string CvName { get; set; }
        public string Introduction { get; set; }
        public string Education { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
