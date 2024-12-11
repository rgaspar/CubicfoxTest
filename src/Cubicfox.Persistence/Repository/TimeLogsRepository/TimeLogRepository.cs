using Cubicfox.Domain.Common.Repository.TimeLogsRepository;
using Cubicfox.Domain.Entities;
using Cubicfox.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cubicfox.Persistence.Repository.TimeLogsRepository;

public class TimeLogRepository(CubicfoxTestContext context) : BaseRepository<TimeLog>(context), ITimeLogRepository
{
    public Task<TimeLog?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _context.TimeLogs.FirstOrDefaultAsync(it => it.Id == id && !it.IsDeleted, cancellationToken);
    }
    
    public Task<TimeLog?> AllTimeLogIsStopped(CancellationToken cancellationToken)
    {
        return _context.TimeLogs.FirstOrDefaultAsync(it => !it.EndDate.HasValue && !it.IsDeleted, cancellationToken);
    }
}
