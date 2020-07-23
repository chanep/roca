using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class TimeSheetRepository : EfGenericRepository<int, TimeSheet>, ITimeSheetRepository
    {
        public TimeSheetRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public TimeSheet GetFull(int id)
        {
            return DbSet.Include(t => t.Items.Select(i => i.Subproject))
                .Include(t => t.Items.Select(i => i.Document))
                .Include(t => t.Items.Select(i => i.Task))
                .Include(t => t.User)
                .Include(t => t.User.Specialties)
                .Include(t => t.Leader)
                .Include(t => t.Specialty)
                .SingleOrDefault(t => t.Id == id);
        }

        public override IQueryable<TimeSheet> GetAll()
        {
            return base.GetAll()
                        .Include(t => t.Specialty)
                        .Include(t => t.User)
                        .Include(t => t.Leader);
        }

        

        public IQueryable<TimeSheet> GetAll(TimeSheetFilter filter)
        {
            return GetAll()
                    .Where(t => (filter.UserId == null || filter.UserId == 0 || t.UserId == filter.UserId.Value) &&
                                (filter.SpecialtyId == null || filter.SpecialtyId == 0 || t.SpecialtyId == filter.SpecialtyId.Value) &&
                                (filter.FromDate == null || t.ControlDate >= filter.FromDate.Value) &&
                                (filter.ToDate == null || t.ControlDate <= filter.ToDate.Value) &&
                                (filter.Status == null || (t.Status & (int)filter.Status.Value) != 0)
                    );
        }

        public IQueryable<TimeSheet> GetAllUserLast()
        {
            var userDate = base.GetAll()
                    .GroupBy(x => x.UserId)
                    .Select(g => new { UserId = g.Key, ControlDate = g.Max(t => t.ControlDate) });
            return base.GetAll().Where(t => userDate.Contains(new { t.UserId, t.ControlDate }));
        }


        public IQueryable<TimeSheetItem> GetAllItems()
        {
            var filter = new TimeSheetItemFilter();
            return GetAllItems(filter);
        }

        public IQueryable<TimeSheetItem> GetAllItems(TimeSheetItemFilter filter)
        {
            return DbSet.Include(t => t.Items)
                .Where(t => (filter.FromDate == null || t.ControlDate >= filter.FromDate.Value) &&
                            (filter.ToDate == null || t.ControlDate <= filter.ToDate.Value))
                .SelectMany(t => t.Items)
                .Include(i => i.TimeSheet.Specialty)
                .Include(i => i.Subproject.Parent);
        }


        public IQueryable<TimeSheetItem> GetAllDocItems()
        {
            var filter = new TimeSheetItemFilter();
            return GetAllDocItems(filter);
        }

        public IQueryable<TimeSheetItem> GetAllDocItems(TimeSheetItemFilter filter)
        {
            return DbSet.Include(t => t.Items)
                .Where(t => (filter.FromDate == null || t.ControlDate >= filter.FromDate.Value) &&
                            (filter.ToDate == null || t.ControlDate <= filter.ToDate.Value))
                .SelectMany(t => t.Items)
                .Where(i => i.DocumentId != null)
                .Include(i => i.Document.Project.Parent)
                .Include(i => i.Document.Specialty)
                .Include(i => i.TimeSheet.User);
        }

        public IQueryable<TimeSheetItem> GetAllTaskItems()
        {
            var filter = new TimeSheetItemFilter();
            return GetAllTaskItems(filter);
        }


        public IQueryable<TimeSheetItem> GetAllTaskItems(TimeSheetItemFilter filter)
        {
            return DbSet.Include(t => t.Items)
                .Where(t => (filter.FromDate == null || t.ControlDate >= filter.FromDate.Value) &&
                            (filter.ToDate == null || t.ControlDate <= filter.ToDate.Value))
                .Include(t => t.User)
                .SelectMany(t => t.Items)
                .Where(i => i.TaskId != null)
                .Include(i => i.Task)
                .Include(i => i.Subproject.Parent)
                .Include(i => i.TimeSheet.User);
        }

        public IQueryable<TimeSheetItem> GetAllItems(int id)
        {
            return GetAllItems()
                .Where(i => i.TimeSheetId == id);
        }

        public TimeSheetItem GetItem(int itemId)
        {
            return DbSet.Include(t => t.Items)
                .SelectMany(t => t.Items)
                .Include(i => i.Document)
                .Include(i => i.Task)
                .FirstOrDefault(i => i.Id == itemId);
        }

        public TimeSheetItem AddItem(TimeSheetItem item)
        {
            DbContext.Entry(item).State = EntityState.Added;
            return item;
        }

        public void UpdateItem(TimeSheetItem item)
        {
            Update<TimeSheetItem>(item);
        }

        public void DeleteItem(TimeSheetItem item)
        {
            if (item.TimeSheet != null)
                item.TimeSheet.Items.Remove(item);
            var entry = DbContext.Entry(item);
            entry.State = EntityState.Deleted;
        }

        public void DeleteItem(int itemId)
        {
            var item = GetItem(itemId);
            if (item == null)
                throw new RocaException(string.Format("No existe el TimeSheetItem de Id: {0}", itemId));
            DeleteItem(item);
        }

        protected void NullifyReferences(TimeSheet ts)
        {

        }
    }
}