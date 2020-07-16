using System;
using System.Collections.Generic;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.Core.Entity
{
    public interface IDal<in K, T> where T : Entity<K>
    {
        T Get(K id);
        IList<T> GetAll();
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
