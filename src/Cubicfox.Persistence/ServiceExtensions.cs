using Cubicfox.Application.Repository;
using Cubicfox.Domain.Common.Repository.TimeLogsRepository;
using Cubicfox.Persistence.Context;
using Cubicfox.Persistence.Repository;
using Cubicfox.Persistence.Repository.TimeLogsRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cubicfox.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CubicfoxTestContext>(options => options.UseSqlServer(connection));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITimeLogRepository, TimeLogRepository>();
    }
}
