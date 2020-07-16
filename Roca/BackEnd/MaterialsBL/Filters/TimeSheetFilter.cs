using System;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.BL.Filters
{
    public class TimeSheetFilter
    {
        public int? UserId { get; set; }
        public int? SpecialtyId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSheetStatus? Status { get; set; }
    }
}
