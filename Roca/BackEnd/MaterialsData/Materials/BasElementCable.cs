using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementCable : BasElementEi
    {
        protected override string GetFieldDescription(BasCodeField field)
        {
            if (field.BasCode.Description == "-")
                return "";
            string desc = field.BasCode.Description;
            if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                desc = desc.Replace("(", "").Replace(")", "");
            return AppendSeparator(desc);
        }

        protected override string GetFieldShortDescription(BasCodeField field)
        {
            if (field.BasCode.ShortDescription == "-")
                return "";
            string desc = field.BasCode.ShortDescription;
            if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                desc = desc.Replace("(", "").Replace(")", "");
            return AppendSeparator(desc);
        }

        protected override string GetDimUnit(int index)
        {
            var fields = Fields.ToList();
            int i = 0;
            for (int j = 0; j < fields.Count; j++)
            {
                var field = fields[j];
                if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                {
                    if (index == i)
                    {
                        return field.BasCode.ShortDescription;
                    }
                    i++;
                }
            }
            return "";
        }
    }
}
