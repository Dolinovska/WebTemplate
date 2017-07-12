using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebTemplate.IRepositories;
using WebTemplate.Models;

namespace WebTemplate.Database.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual T Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
