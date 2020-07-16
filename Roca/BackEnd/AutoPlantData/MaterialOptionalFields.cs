using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.AutoPlant.Data
{
    [Flags]
    public enum MaterialOptionalFields
    {
        None = 0,
        Service = 1,
        Line = 2,
        Tag = 4,
        All = 0xFFFF
    }
}
