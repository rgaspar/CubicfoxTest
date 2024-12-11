using System.Linq.Expressions;
using Cubicfox.Domain.Common.Entity;
using Cubicfox.Domain.Common.Models;

namespace Cubicfox.Domain.Common.Repository;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> Get(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<Pagination<TResult>> ToPagination<TResult>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Expression<Func<T, object>>? orderBy = null,
        bool ascending = true,
        Expression<Func<T, TResult>>? selector = null);
}
