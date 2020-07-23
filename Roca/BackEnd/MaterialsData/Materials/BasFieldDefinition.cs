using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasFieldDefinition : Entity<int>
    {
        public int Order { get; set; }
        public int Length { get; set; }
        public int TypeInt { get; set; }
        public int ElementTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public BasFieldType Type
        {
            get { return (BasFieldType) TypeInt; }
        }

        public new BasFieldDefinition Clone()
        {
            var clone = (BasFieldDefinition) MemberwiseClone();
            return clone;
        }

    }
}
