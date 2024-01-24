using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.Entities;

public partial class Cv
{
    public Guid Cvid { get; set; }

    public Guid CandidateId { get; set; }

    public string? Experience { get; set; }

    public string? CvPdf { get; set; }

    [Required]
    [DataType(DataType.ImageUrl)]
    public string CvName { get; set; }

    [Required]
    public string Introduction { get; set; } = null!;

    [Required]
    public string Education { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public bool IsDefault { get; set; } = false;

    public virtual ICollection<Application> Applications { get; set; } = null!;

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual ICollection<Certificate> Certificates { get; set; } = null!;

    public virtual ICollection<CvHasSkill> CvHasSkills { get; set; } = null!;
}
