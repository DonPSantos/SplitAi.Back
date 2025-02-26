using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task Delete(Guid id)
        {
            _dbSet.Remove(new T { Id = id });
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                             params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Aplica os Includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Aplica o filtro (WHERE)
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Aplica a ordenação (ORDER BY)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}