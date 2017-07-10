using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebTemplate.IRepositories;
using WebTemplate.Models;

namespace WebTemplate.Database
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : HasIdentity

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

        public virtual T Find(int id)
        {
            return _dbSet.Find(id);
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

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
