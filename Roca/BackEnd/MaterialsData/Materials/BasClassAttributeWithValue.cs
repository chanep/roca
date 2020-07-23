using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasClassAttributeWithValue : BasClassAttribute
    {
        public string Value { get; set; }

        public BasClassAttributeWithValue()
        {
            Value = "";
        }

        public BasClassAttributeWithValue(BasClassAttribute attribute)
        {
            Id = attribute.Id;
            Type = attribute.Type;
            ClassId = attribute.ClassId;
            Name = attribute.Name;
            Property = attribute.Property;
            Value = "";
        }
    }
}
