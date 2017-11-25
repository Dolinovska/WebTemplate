namespace WebTemplate.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class Repository
    {
        private readonly DbContext _context;

        public Repository()
        {
            this._context = new WebTemplateContext();
        }

        public void Add<T>(T entity) where T : class
        {
            var dbSet = this._context.Set<T>();
            dbSet.Add(entity);
        }

        public T Find<T>(params object[] keyValues) where T : class
        {
            var dbSet = this._context.Set<T>();
            return dbSet.Find(keyValues);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            var dbSet = this._context.Set<T>();
            return dbSet;
        }

        public IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class
        {
            var dbSet = this._context.Set<T>();
            return dbSet.Where(predicate);
        }

        public void Update<T>(T entity) where T : class
        {
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            var dbSet = this._context.Set<T>();
            dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
    }
}