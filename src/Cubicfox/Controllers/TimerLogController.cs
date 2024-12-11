using Cubicfox.Application.Service.TimeLog.Interface;
using Cubicfox.Application.Service.Zenquotes.Interface;
using Cubicfox.Domain.Common.Request.TimeLog;
using Microsoft.AspNetCore.Mvc;

namespace Cubicfox.Controllers;

[ApiController]
public class TimerLogController : BaseController
{
    private readonly ILogger<TimerLogController> _logger;
    private readonly ITimeLogService _timeLogService;
    private readonly IZenquotesService _zenquotesService;
    
    public TimerLogController(ITimeLogService timeLogService, IZenquotesService zenquotesService, ILogger<TimerLogController> logger)
    {
        _logger = logger;
        _timeLogService = timeLogService;
        _zenquotesService = zenquotesService;
    }

    [HttpGet(Name = "FindTimeLog/{id}")]
    public async Task<IActionResult> Find(Guid id, CancellationToken token)
    {
        var result = await _timeLogService.FindAsync(id, token);
        
        return Ok(result);
    }

    [HttpGet(Name = "GetAllTimerLogs/{pageIndex:int}/{pageSize:int}")]
    public async Task<IActionResult> GetAll(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, CancellationToken token)
    {
        var request = new GetAllTimeLogRequest(startDate, endDate, pageIndex, pageSize);
        var result = await _timeLogService.GetAllAsync(request);
        return Ok(result);
    }
    
    [HttpPost(Name = "StartTimeLog")]
    public async Task<IActionResult> StartTimeLog(CancellationToken token)
    {
        var zenquotes = await _zenquotesService.GetAsync(token);
        var request = new StartTimeLogRequest(zenquotes.Description);
        
        var result = await _timeLogService.StartAsync(request, token);
        return Ok(result);
        
    }
    
    [HttpPut(Name = "StopTimeLog")]
    public async Task<IActionResult> StopTimeLog(StopTimeLogRequest request, CancellationToken token)
    {
        var result = await _timeLogService.StopAsync(request, token);
        return Ok(result);
        
    }
    
    [HttpPut(Name = "UpdateTimeLog")]
    public async Task<IActionResult> UpdateTimeLog(UpdateTimeLogRequest request, CancellationToken token)
    {
        var result = await _timeLogService.UpdateAsync(request, token);
        return Ok(result);
        
    }   
    
    [HttpDelete(Name = "DeleteTimeLog")]
    public async Task<IActionResult> DeleteTimeLog(Guid id, CancellationToken token)
    {
        var result = await _timeLogService.DeleteAsync(id, token);
        return Ok(result);
        
    }   
    
}
