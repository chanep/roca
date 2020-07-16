using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    /// <summary>
    /// Tipo de Material tagueable
    /// </summary>
    public class TaggableType : Entity<int>
    {
        public TaggableType()
        {
            Subtypes = new List<TaggableType>();
            Attributes = new List<TaggableAttribute>();
        }

        public int SpecialtyId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<TaggableType> Subtypes { get; set; }
        public virtual ICollection<TaggableAttribute> Attributes { get; set; }
    }
}
