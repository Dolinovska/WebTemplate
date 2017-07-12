using System;
using System.Collections.Generic;

namespace WebTemplate.IServices
{
    // should not be changed, just some basic CRUD
    public interface IGenericService<T>
    {
        void Create(T entity);
        T Find(params object[] keyValues);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Update(T entity);
        void Delete(T entity);
    }
}
