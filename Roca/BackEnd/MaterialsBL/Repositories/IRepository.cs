using System.Linq;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IRepository<K, T> where T : Entity<K>, new()
    {
        
        T Get(K id);
        IQueryable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(K id);
    }
}