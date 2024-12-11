namespace Cubicfox.Domain.Common.Request.TimeLog;

public class GetAllTimeLogRequest(DateTime startDate, DateTime endDate, int pageIndex, int pageSize)
{
    public DateTime? StartDate { get; set; } = startDate;
    public DateTime? EndDate { get; set; } = endDate;
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
}
