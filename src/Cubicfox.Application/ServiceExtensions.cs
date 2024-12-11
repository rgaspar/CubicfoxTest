using Cubicfox.Application.Service.TimeLog.Interface;
using Cubicfox.Application.Service.TimeLog.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Cubicfox.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<ITimeLogService, TimeLogService>();
    }
}
