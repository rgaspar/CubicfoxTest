using Cubicfox.Domain.Common.Entity;

namespace Cubicfox.Domain.Entities;

public class TimeLog : BaseEntity
{
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
