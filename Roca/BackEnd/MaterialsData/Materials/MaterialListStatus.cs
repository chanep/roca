using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    [Flags]
    public enum MaterialListStatus : int
    {
        Elaboration = 1,
        Issued = 2,
        Superseded = 4
    }
}
