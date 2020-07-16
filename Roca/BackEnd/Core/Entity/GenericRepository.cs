using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.Core.Entity
{
    public class GenericRepository<K, T> where T : Entity<K>, new()
    {
        protected DalBasic<K, T> Dal { get; set; }

        public GenericRepository(DalBasic<K, T> dal)
        {
            Dal = dal;
        }
        public T Get(K id)
        {
            return Dal.Get(id);
        }

        public IList<T> GetAll()
        {
            return Dal.GetAll();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> condition, params Expression<Func<T, Direction>>[] orders)
        {
            return Dal.GetAll(condition, orders);
        }

        public T Create(T entity)
        {
            return Dal.Create(entity);
        }

        public void Update(T entity)
        {
            Dal.Update(entity);
        }

        public void Delete(T entity)
        {
            Dal.Delete(entity);
        }
    }

}
