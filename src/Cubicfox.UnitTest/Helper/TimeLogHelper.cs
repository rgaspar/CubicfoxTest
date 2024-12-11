using Cubicfox.Domain.Common.Response.TimeLog;
using Cubicfox.Domain.Entities;

namespace Cubicfox.UnitTest.Helper;

public static class TimeLogHelper
{
    public static TimeLog TestTimeLog()
    {
        var result = new TimeLog
        {
            Id = Guid.NewGuid(),
            Description = "First - Test",
            StartDate = DateTime.Now,
            EndDate = null,
            IsDeleted = false,
        };
        
        return result;
    } 
    
    public static TimeLogResponse TestTimeLogResponse()
    {
        var result = new TimeLogResponse
        {
            Id = Guid.NewGuid(),
            Description = "First - Test",
            StartDate = DateTime.Now,
            EndDate = null,
            IsDeleted = false,
        };
        
        return result;
    } 
    
    public static List<TimeLogResponse> TestTimeLogsResponse()
    {
        var result = new List<TimeLogResponse> { TestTimeLogResponse(), TestTimeLogResponse() };
        
        return result;
    } 
    
    public static TimeLog CreateFrom(TimeLog timeLog)
    {
        var result = new TimeLog
        {
            Id = timeLog.Id,
            Description = timeLog.Description,
            StartDate = timeLog.StartDate,
            EndDate = timeLog.EndDate,
            IsDeleted = timeLog.IsDeleted,
        };
        
        return result;
    } 
}
