using Cubicfox.Application.Repository;
using Cubicfox.Application.Service.TimeLog.Interface;
using Cubicfox.Domain.Common.Exceptions;
using Cubicfox.Domain.Common.Models;
using Cubicfox.Domain.Common.Repository.TimeLogsRepository;
using Cubicfox.Domain.Common.Request.TimeLog;
using Cubicfox.Domain.Common.Response.TimeLog;
using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Application.Service.TimeLog.Service;

public class TimeLogService(IUnitOfWork unitOfWork, ITimeLogRepository timeLogRepository)
    : ITimeLogService
{
    public async Task<Pagination<TimeLogResponse>> GetAllAsync(GetAllTimeLogRequest request)
    {
        var timeLogs = await timeLogRepository.ToPagination(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            filter: x => x.StartDate >= request.StartDate && x.EndDate <= request.EndDate,
            orderBy: x => x.Id,
            ascending: true,
            selector: x => TimeLogResponse.FromModel(x)
        );

        return timeLogs;
    }

    public async Task<TimeLogResponse> FindAsync(Guid id, CancellationToken token)
    {
        var timeLog = await timeLogRepository.GetById(id, token);
        return timeLog == null ? null! : TimeLogResponse.FromModel(timeLog);
    }

    public async Task<TimeLogResponse> StartAsync(StartTimeLogRequest request, CancellationToken token)
    {
        if (await timeLogRepository.AllTimeLogIsStopped(token) != null)
        {
            throw new CubicfoxException("A timer is already running", "A timer is already running");
        } 
        
        var model = StartTimeLogRequest.ToModel(request.Description); 
        timeLogRepository.Create(model);
        await unitOfWork.Save(token);
        var response = TimeLogResponse.FromModel(model);
        return response;
    }
    
    public async Task<TimeLogResponse> StopAsync(StopTimeLogRequest request, CancellationToken token)
    {
        var existTimeLog = await timeLogRepository.GetById(request.Id, token) ?? 
                           throw new CubicfoxException("Timer is not found", "Timer is not found");
         
        existTimeLog.EndDate = CurrentTime.GetCurrentTime();
        
        timeLogRepository.Update(existTimeLog);
        await unitOfWork.Save(token);
        
        var response = TimeLogResponse.FromModel(existTimeLog);
        return response;
    }

    public async Task<TimeLogResponse> UpdateAsync(UpdateTimeLogRequest request, CancellationToken token)
    {
        var existTimeLog = await timeLogRepository.GetById(request.Id, token) ?? 
                           throw new CubicfoxException("Timer is not found", "Timer is not found");
        
        existTimeLog.Description = request.Description;
        
        timeLogRepository.Update(existTimeLog);
        await unitOfWork.Save(token);
        
        var response = TimeLogResponse.FromModel(existTimeLog);
        return response;
    }

    public async Task<TimeLogResponse> DeleteAsync(Guid id, CancellationToken token)
    {
        var existTimeLog = await timeLogRepository.GetById(id, token) ?? 
            throw new CubicfoxException("Timer is not found", "Timer is not found");
        
        existTimeLog.IsDeleted = true;
        timeLogRepository.Delete(existTimeLog);
        
        await unitOfWork.Save(token);
        
        var response = TimeLogResponse.FromModel(existTimeLog);
        
        return response;
    }
}
