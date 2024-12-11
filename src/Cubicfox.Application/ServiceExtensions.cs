using Cubicfox.Application.Service.TimeLog.Interface;
using Cubicfox.Application.Service.TimeLog.Service;
using Cubicfox.Application.Service.Zenquotes.Interface;
using Cubicfox.Application.Service.Zenquotes.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Cubicfox.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<ITimeLogService, TimeLogService>();
        services.AddScoped<IZenquotesService, ZenquotesService>();
    }
}
