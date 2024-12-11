using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Domain.Common.Request.TimeLog;

public abstract class StopTimeLogRequest
{
    public Guid Id { get; set; }
}
