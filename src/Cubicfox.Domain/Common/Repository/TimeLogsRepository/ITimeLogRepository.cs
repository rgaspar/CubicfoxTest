using Cubicfox.Domain.Entities;

namespace Cubicfox.Domain.Common.Repository.TimeLogsRepository;

public interface ITimeLogRepository: IBaseRepository<TimeLog>
{
    Task<TimeLog?> GetById(Guid id, CancellationToken cancellationToken);
    
    Task<TimeLog?> AllTimeLogIsStopped(CancellationToken cancellationToken);
}
