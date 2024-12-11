using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Domain.Common.Request.TimeLog;

public class StopTimeLogRequest(Guid id)
{
    public Guid Id { get; set; } = id;
}
