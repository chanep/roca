using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.TimeSheets
{
    [Flags]
    public enum TimeSheetStatus: int
    {
        Open = 1,
        Closed = 2
    }
}
