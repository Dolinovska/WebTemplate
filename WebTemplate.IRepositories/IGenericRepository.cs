using System;
using System.Collections.Generic;

namespace WebTemplate.IRepositories
{
    // should not be changed, just some basic CRUD
    public interface IGenericRepository<T>
    {
        void Create(T entity);
        T Find(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Update(T entity);
        void Remove(T entity);
    }
}
