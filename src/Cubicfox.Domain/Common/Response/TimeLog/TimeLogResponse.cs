namespace Cubicfox.Domain.Common.Response.TimeLog;

public class TimeLogResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDeleted { get; set; }

    public static TimeLogResponse FromModel(Entities.TimeLog model)
    {
        return new TimeLogResponse()
        {
            Id = model.Id,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            IsDeleted = model.IsDeleted,
        };
    }
}
