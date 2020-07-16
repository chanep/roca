using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface ITimeSheetService
    {
        TimeSheet Get(int id);
        TimeSheet GetFull(int id);
        TimeSheet GetFull(int userId, int specialtyId, DateTime controlDate);
        IEnumerable<TimeSheet> GetAllByUser(int userId);
        TimeSheet Add(TimeSheet timeSheet);
        void Update(TimeSheet timeSheet);

        IEnumerable<TimeSheetItem> GetAllItems();
        IEnumerable<TimeSheetItem> GetAllDocItems();
        IEnumerable<TimeSheetItem> GetAllTaskItems();
        IEnumerable<TimeSheetItem> GetAllItems(TimeSheetItemFilter filter);
        IEnumerable<TimeSheetItem> GetAllDocItems(TimeSheetItemFilter filter);
        IEnumerable<TimeSheetItem> GetAllTaskItems(TimeSheetItemFilter filter);
        IEnumerable<TimeSheetItem> GetAllItems(int id);


        void SaveItems(int timeSheetId, IEnumerable<TimeSheetItem> items);

        TimeSheet Save(TimeSheet timeSheet);
        TimeSheet GetLast(int userId, int? specialtyId);
        IEnumerable<TimeSheet> GetAll(TimeSheetFilter filter);
        IEnumerable<User> GetDefaulters(DateTime fromDate, DateTime toDate);
        IEnumerable<TimeSheet> GetAllUserLast();
        void UpdateAll(IEnumerable<TimeSheet> timeSheets);
    }
}
