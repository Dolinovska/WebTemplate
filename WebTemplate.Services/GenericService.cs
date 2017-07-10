using System;
using System.Collections.Generic;
using WebTemplate.IRepositories;
using WebTemplate.IServices;

namespace WebTemplate.Services
{
    public abstract class GenericService<T> : IGenericService<T>
    {
        protected readonly IGenericRepository<T> Repository;

        protected GenericService(IGenericRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual void Create(T entity)
        {
            Repository.Create(entity);
        }

        public virtual T Find(int id)
        {
            return Repository.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Repository.Find(predicate);
        }

        public virtual void Update(T entity)
        {
            Repository.Update(entity);
        }

        public void Remove(T entity)
        {
            Repository.Remove(entity);
        }
    }
}