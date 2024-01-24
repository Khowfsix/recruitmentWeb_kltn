using InsternShip.Data.Entities;

namespace InsternShip.Data.Models;

public partial class RecruiterModel
{
    public Guid RecruiterId { get; set; }

    public string UserId { get; set; }

    public virtual WebUser User { get; set; }

    public Guid DepartmentId { get; set; }

    public bool IsDeleted { get; set; } = false;

}
