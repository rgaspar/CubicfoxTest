using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Domain.Common.Request.TimeLog;

public abstract class UpdateTimeLogRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    
    public static Entities.TimeLog ToModel(Guid id, string description)
    {
        var model = new Entities.TimeLog
        {
            Id = id, Description = description, StartDate = CurrentTime.GetCurrentTime(), IsDeleted = false
        };

        return model; 
    }
}
