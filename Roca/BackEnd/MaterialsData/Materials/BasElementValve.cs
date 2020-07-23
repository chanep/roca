using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementValve : BasElement
    {
        public override string Description
        {
            get
            {
                return FamilyDescription;
            }
        }
    }
}
