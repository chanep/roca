using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasCodeField : Entity<int>
    {
        public int ElementId { get; set; }
        public int FieldDefinitionId { get; set; }
        public int BasCodeId { get; set; }
        public BasFieldDefinition FieldDefinition { get; set; }
        public BasCode BasCode { get; set; }
    }
}
