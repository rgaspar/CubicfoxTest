using Cubicfox.Application.Repository;
using Cubicfox.Persistence.Context;

namespace Cubicfox.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly CubicfoxTestContext _context;

    public UnitOfWork(CubicfoxTestContext context)
    {
        _context = context;
    }
    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
