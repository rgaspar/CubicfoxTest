using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Domain.Common.Request.TimeLog;

public abstract class StartTimeLogRequest(string description)
{
    public string Description { get; set; } = description;

    public static Entities.TimeLog ToModel(string description)
    {
        var model = new Entities.TimeLog
        {
            Description = description, StartDate = CurrentTime.GetCurrentTime(), IsDeleted = false
        };

        return model; 
    }
}
