using Microsoft.EntityFrameworkCore;

namespace Cubicfox.Persistence.Context;

public class CubicfoxTestContext: DbContext
{
    public CubicfoxTestContext(DbContextOptions<CubicfoxTestContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.TimeLog> TimeLogs { get; set; }
}
