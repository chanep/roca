using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class EfGenericRepository<K, T> : IRepository<K, T> where T : Entity<K>
    {
        public EfGenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T Get(K id)
        {
            return DbSet.Find(id);
        }

        public virtual T Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
                return entity;
            }
            else
            {
                return DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            Update<T>(entity);
        }

        protected virtual void Update<TEntity>(TEntity entity) where TEntity : Entity<K>
        {
            var attached = DbContext.Set<TEntity>().Local.FirstOrDefault(i => i.Id.Equals(entity.Id));
            if (attached != null)
            {
                var attachedEntry = DbContext.Entry(attached);
                attachedEntry.CurrentValues.SetValues(entity);
            }
            else
            {
                var entry = DbContext.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            //dbEntityEntry.State = EntityState.Deleted;

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);

            //DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            //if (dbEntityEntry.State != EntityState.Deleted)
            //{
            //    dbEntityEntry.State = EntityState.Deleted;
            //}
            //else
            //{
            //    DbSet.Attach(entity);
            //    DbSet.Remove(entity);
            //}
        }

        public virtual void Delete(K id)
        {
            var entity = Get(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
    }
}
