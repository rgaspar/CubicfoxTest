using Cubicfox.Domain.Common.Utils;

namespace Cubicfox.Domain.Common.Request.TimeLog;

public class UpdateTimeLogRequest(Guid id, string description)
{
    public Guid Id { get; set; } = id;
    public string Description { get; set; } = description;
    
    public static Entities.TimeLog ToModel(Guid id, string description)
    {
        var model = new Entities.TimeLog
        {
            Id = id, Description = description, StartDate = CurrentTime.GetCurrentTime(), IsDeleted = false
        };

        return model; 
    }
}
