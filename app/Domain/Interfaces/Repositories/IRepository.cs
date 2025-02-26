using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        Task Update(T entity);

        Task Create(T entity);

        Task Delete(Guid id);

        Task<T> GetById(Guid id);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    params Expression<Func<T, object>>[] includes);

        Task<int> SaveChanges();
    }
}