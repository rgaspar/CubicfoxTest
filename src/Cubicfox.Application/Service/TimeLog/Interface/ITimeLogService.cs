using Cubicfox.Domain.Common.Models;
using Cubicfox.Domain.Common.Request.TimeLog;
using Cubicfox.Domain.Common.Response.TimeLog;

namespace Cubicfox.Application.Service.TimeLog.Interface;

public interface ITimeLogService
{
    Task<Pagination<TimeLogResponse>> GetAllAsync(GetAllTimeLogRequest request);
    Task<TimeLogResponse> FindAsync(Guid id, CancellationToken token);
    Task<TimeLogResponse> StartAsync(StartTimeLogRequest request, CancellationToken token);
    Task<TimeLogResponse> StopAsync(StopTimeLogRequest request, CancellationToken token);
    Task<TimeLogResponse> UpdateAsync(UpdateTimeLogRequest request, CancellationToken token);
    Task<TimeLogResponse> DeleteAsync(Guid id, CancellationToken token);
}
