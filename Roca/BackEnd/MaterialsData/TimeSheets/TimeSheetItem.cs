using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.TimeSheets
{
    public class TimeSheetItem : Entity<int>
    {
        public int TimeSheetId { get; set; }
        public int SubprojectId { get; set; }
        public int? DocumentId { get; set; }
        public int? TaskId { get; set; }
        public int Hours { get; set; }

        public TimeSheet TimeSheet { get; set; }
        public Project Subproject { get; set; } 
        public Document Document { get; set; }
        public LookUp Task { get; set; }
    }
}
