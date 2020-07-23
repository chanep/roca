using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementType : Entity<int>
    {
        private ICollection<BasFieldDefinition> _fieldDefinitions;

        public string Code { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public BasClass Class { get; set; }

        public ICollection<Specialty> Specialties { get; set; }

        public virtual ICollection<BasFieldDefinition> FieldDefinitions
        {
            get
            {
                _fieldDefinitions = _fieldDefinitions.OrderBy(f => f.Order).ToList();
                return _fieldDefinitions;
            }
            set
            {
                _fieldDefinitions = value;
            }
        }

        public BasElementType()
        {
            FieldDefinitions = new List<BasFieldDefinition>();
            Specialties = new List<Specialty>();
        }
    }
}
