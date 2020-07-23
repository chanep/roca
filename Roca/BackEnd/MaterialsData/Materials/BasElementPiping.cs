using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementPiping : BasElement
    {
        public override string Description
        {
            get
            {
                return FamilyDescription;
            }
        }

        public override string ShortDescription
        {
            get
            {
                return ShortFamilyDescription;
            }
        }

        protected override string GetDim(int index)
        {
            if (index != 2)
                return base.GetDim(index);
            return GetDim3();
        }

        protected override string GetUnit(int index)
        {
            if (index != 2)
                return base.GetUnit(index);
            return GetUnit3();
        }

        private string GetDim3()
        {
            var dimUnit3 = GetDimUnit(2);
            var dimUnit4 = GetDimUnit(3);
            string dim31 = ExtractDim(dimUnit3);
            string dim32 = ExtractDim(dimUnit4);
            if (dim31 != "" && dim32 != "")
                return dim31 + " / " + dim32;
            return dim31 + dim32;
        }

        private string GetUnit3()
        {
            var dimUnit3 = GetDimUnit(2);
            var dimUnit4 = GetDimUnit(3);
            string unit31 = ExtractUnit(dimUnit3);
            string unit32 = ExtractUnit(dimUnit4);
            if (unit31 != "" && unit32 != "")
            {
                if (unit31 != unit32)
                    return unit31 + " / " + unit32;
                return unit31;
            }
            return unit31 + unit32;
        }
    }
}
