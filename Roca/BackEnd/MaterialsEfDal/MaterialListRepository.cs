using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.SqlServer.Server;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class MaterialListRepository : EfGenericRepository<int, MaterialList>, IMaterialListRepository
    {
 
        public MaterialListRepository(DbContext dbContext) : base(dbContext)
        {
            //Para que los query no devuelvan las listas de materiales borradas logicamente (deleted == true)
            DbSet = new FilteredDbSet<MaterialList>(DbContext, l => l.DeletedInt == 0, null);
        }

        public MaterialList GetFull(int id)
        {
            return DbSet.Include(m => m.Items)
                .Include(m => m.Project)
                .Include(m => m.Approver)
                .Include(m => m.Creator)
                .Include(m => m.Revisor)
                .Include(m => m.Specialty)              
                .Include(m => m.Items.Select(i => i.Material))
                .SingleOrDefault(m => m.Id == id);
        }

        public override IQueryable<MaterialList> GetAll()
        {
            return base.GetAll().Include(ml => ml.Project)
                        .Include(ml => ml.Specialty)
                        .Include(ml => ml.Creator);
        }

        

        public void LogicalDelete(int id)
        {
            var ml = Get(id);
            ml.Deleted = true;
            Update(ml);
        }


        public IQueryable<MlItem> GetAllItems(int id)
        {
            return DbSet.Include(l => l.Items).SelectMany(ml => ml.Items).Include(i => i.Material).Where(i => i.MlId == id);
        }

        public MlItem GetItem(int itemId)
        {
            return DbSet.Include(l => l.Items).SelectMany(ml => ml.Items).FirstOrDefault(i => i.Id == itemId);
        }

        public MlItem AddItem(MlItem item)
        {
            DbContext.Entry(item).State = EntityState.Added;
            return item;
        }


        public void UpdateItem(MlItem item)
        {
            Update<MlItem>(item);
        }

        public void DeleteItem(MlItem item)
        {
            var entry = DbContext.Entry(item);
            entry.State = EntityState.Deleted;
        }

        public void DeleteItem(int itemId)
        {
            var item = GetItem(itemId);
            if(item == null)
                throw new RocaException(string.Format("No existe el MlItem de Id: {0}", itemId));
            DeleteItem(item);
        }

    }
}
