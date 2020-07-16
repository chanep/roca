using System;
using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.TimeSheets
{
    public class TimeSheet : Entity<int>
    {
        public int UserId { get; set; }
        public int? LeaderId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime ControlDate { get; set; }
        public int Status { get; set; }

        public User User { get; set; }
        public User Leader { get; set; }
        public Specialty Specialty { get; set; }

        public ICollection<TimeSheetItem> Items { get; set; }

        public TimeSheet()
        {
            Items = new List<TimeSheetItem>();
        }

        public static DateTime GetNextFriday(DateTime date)
        {
            int daysUntilFriday = ((int)DayOfWeek.Friday - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(daysUntilFriday).Date;
        }

    }
}
