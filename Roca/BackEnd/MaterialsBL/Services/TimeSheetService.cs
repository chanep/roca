using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class TimeSheetService : BaseService, ITimeSheetService
    {
        public TimeSheetService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        public TimeSheet Get(int id)
        {
            return RocaUow.TimeSheets.Get(id);
        }

        public TimeSheet GetFull(int id)
        {
            return RocaUow.TimeSheets.GetFull(id);
        }

        public TimeSheet GetFull(int userId, int specialtyId, DateTime controlDate)
        {
            var date = TimeSheet.GetNextFriday(controlDate);
            var ts = RocaUow.TimeSheets.GetAll()
                    .FirstOrDefault(t => t.UserId == userId && t.ControlDate == date && t.SpecialtyId == specialtyId);
            if (ts == null)
                return null;
            return GetFull(ts.Id);
        }

        public IEnumerable<TimeSheet> GetAll(TimeSheetFilter filter)
        {
            return RocaUow.TimeSheets.GetAll(filter);
        }

        public IEnumerable<TimeSheet> GetAllByUser(int userId)
        {
            return RocaUow.TimeSheets.GetAll().Where(t => t.UserId == userId);
        }

        public TimeSheet GetLast(int userId, int? specialtyId)
        {
            var ts = RocaUow.TimeSheets.GetAll()
                        .Where(t => t.UserId == userId && (specialtyId == null || specialtyId.Value == 0 || t.SpecialtyId == specialtyId))
                        .OrderByDescending(t => t.ControlDate)
                        .FirstOrDefault();
            if (ts == null)
                return null;
            return GetFull(ts.Id);
        }

        public IEnumerable<User> GetDefaulters(DateTime fromDate, DateTime toDate)
        {

            var filter = new TimeSheetFilter() {FromDate = fromDate, ToDate = toDate};
            var userIds = RocaUow.TimeSheets.GetAll(filter).Select(t => t.UserId);
            var defaulters = RocaUow.Users.GetAll().Where(d => !userIds.Contains(d.Id));
            return defaulters;
        }

        public IEnumerable<TimeSheet> GetAllUserLast()
        {
            return RocaUow.TimeSheets.GetAllUserLast().AsEnumerable();
        }

        public TimeSheet Add(TimeSheet timeSheet)
        {
            AddInternal(timeSheet);
            RocaUow.Commit();
            return timeSheet;
        }

        public void Update(TimeSheet timeSheet)
        {
            RocaUow.TimeSheets.Update(timeSheet);
            RocaUow.Commit();
        }

        public void UpdateAll(IEnumerable<TimeSheet> timeSheets)
        {
            foreach (var timeSheet in timeSheets)
            {
                RocaUow.TimeSheets.Update(timeSheet);
            }        
            RocaUow.Commit();
        }

        public IEnumerable<TimeSheetItem> GetAllItems()
        {
            return RocaUow.TimeSheets.GetAllItems();
        }

        public IEnumerable<TimeSheetItem> GetAllItems(TimeSheetItemFilter filter)
        {
            return RocaUow.TimeSheets.GetAllItems(filter);
        }

        public IEnumerable<TimeSheetItem> GetAllDocItems()
        {
            return RocaUow.TimeSheets.GetAllDocItems();
        }

        public IEnumerable<TimeSheetItem> GetAllDocItems(TimeSheetItemFilter filter)
        {
            return RocaUow.TimeSheets.GetAllDocItems(filter);
        }

        public IEnumerable<TimeSheetItem> GetAllTaskItems()
        {
            return RocaUow.TimeSheets.GetAllTaskItems();
        }

        public IEnumerable<TimeSheetItem> GetAllTaskItems(TimeSheetItemFilter filter)
        {
            return RocaUow.TimeSheets.GetAllTaskItems(filter);
        }

        public IEnumerable<TimeSheetItem> GetAllItems(int id)
        {
            return RocaUow.TimeSheets.GetAllItems(id).AsEnumerable();
        }

        public TimeSheet Save(TimeSheet timeSheet)
        {
            var items = timeSheet.Items;
            timeSheet.Items = null;
            if (timeSheet.Id == 0)
            {           
                AddInternal(timeSheet);
            }
            else
            {
                RocaUow.TimeSheets.Update(timeSheet);
            }
            SaveItemsInternal(timeSheet.Id, items);
            //timeSheet = Get(timeSheet.Id);
            RocaUow.Commit();
            return timeSheet;
        }

        public void SaveItems(int timeSheetId, IEnumerable<TimeSheetItem> items)
        {
            SaveItemsInternal(timeSheetId, items);
            RocaUow.Commit();
        }

        protected void AddInternal(TimeSheet timeSheet)
        {
            timeSheet.ControlDate = TimeSheet.GetNextFriday(timeSheet.ControlDate);

            bool duplicated = RocaUow.TimeSheets.GetAll()
                .Any(t => t.UserId == timeSheet.UserId && t.SpecialtyId == timeSheet.SpecialtyId &&
                          t.ControlDate == timeSheet.ControlDate);

            if (duplicated)
                throw new RocaException("Ya existe una TimeSheet del usuario con la misma fecha");

            RocaUow.TimeSheets.Add(timeSheet);
        }

        protected void SaveItemsInternal(int timeSheetId, IEnumerable<TimeSheetItem> items)
        {
            var itemList = items.ToList();
            int hours = 0;
            foreach (var item in itemList)
            {
                if (item.TaskId != null && item.DocumentId != null)
                    throw new RocaException("Un TimeSheetItem no puede tener una actividad y un documento simultaneamente");
                if (item.TaskId == null && item.DocumentId == null)
                    throw new RocaException("Un TimeSheetItem debe tener una actividad o un documento");
                hours += item.Hours;
            }
            if (hours > 40)
                throw new RocaException("Las horas de una TimeSheet no deben sumar mas que 40");

            var timeSheet = RocaUow.TimeSheets.Get(timeSheetId);
            if (timeSheet == null)
                throw new RocaException("La TimeSheet no existe. Id: " + timeSheetId);


            var oldItems = RocaUow.TimeSheets.GetAllItems(timeSheetId).ToList();
            foreach (var item in oldItems)
            {
                RocaUow.TimeSheets.DeleteItem(item);
            }

            foreach (var item in itemList)
            {
                item.Id = 0;
                RocaUow.TimeSheets.AddItem(item);
            }
        }


    }
}
