using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface ITimeSheetRepository : IRepository<int, TimeSheet>
    {
        TimeSheet GetFull(int id);
        IQueryable<TimeSheetItem> GetAllItems();
        IQueryable<TimeSheetItem> GetAllDocItems();
        IQueryable<TimeSheetItem> GetAllTaskItems();
        IQueryable<TimeSheetItem> GetAllItems(TimeSheetItemFilter filter);
        IQueryable<TimeSheetItem> GetAllDocItems(TimeSheetItemFilter filter);
        IQueryable<TimeSheetItem> GetAllTaskItems(TimeSheetItemFilter filter);
        IQueryable<TimeSheetItem> GetAllItems(int id);
        TimeSheetItem GetItem(int itemId);
        TimeSheetItem AddItem(TimeSheetItem item);
        void UpdateItem(TimeSheetItem item);
        void DeleteItem(TimeSheetItem item);
        void DeleteItem(int itemId);


        IQueryable<TimeSheet> GetAll(TimeSheetFilter filter);
        IQueryable<TimeSheet> GetAllUserLast();
    }
}
