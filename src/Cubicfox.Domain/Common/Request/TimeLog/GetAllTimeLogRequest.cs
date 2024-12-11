namespace Cubicfox.Domain.Common.Request.TimeLog;

public abstract class GetAllTimeLogRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
