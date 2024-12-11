using System.Linq.Expressions;
using Cubicfox.Domain.Common.Entity;
using Cubicfox.Domain.Common.Models;
using Cubicfox.Domain.Common.Repository;
using Cubicfox.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cubicfox.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly CubicfoxTestContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(CubicfoxTestContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Create(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Update(entity);
    }

    public Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return _dbSet.ToListAsync(cancellationToken);
    }
    
    public async Task<Pagination<TResult>> ToPagination<TResult>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Expression<Func<T, object>>? orderBy = null,
        bool ascending = true,
        Expression<Func<T, TResult>>? selector = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (include != null)
        {
            query = include(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        orderBy ??= x => EF.Property<object>(x, "Id");

        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        var projectedQuery = query.Select(selector);

        var result = await Pagination<TResult>.ToPagedList(projectedQuery, pageIndex, pageSize);

        return result;
    }
}
