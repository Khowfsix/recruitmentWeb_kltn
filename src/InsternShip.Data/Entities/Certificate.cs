using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.Entities;

public partial class Certificate
{
    public Guid CertificateId { get; set; }

    public string CertificateName { get; set; } = null!;

    public string? Description { get; set; }

    public string? OrganizationName { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateEarned { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ExpirationDate { get; set; }

    [DataType(DataType.Url)]
    public string? Link { get; set; }

    public Guid Cvid { get; set; }

    public virtual Cv Cv { get; set; } = null!;
}
