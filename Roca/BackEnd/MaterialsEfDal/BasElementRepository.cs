using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class BasElementRepository : EfGenericRepository<int, BasElement>, IBasElementRepository
    {
        private IList<BasElementType> _elementTypes; 

        public BasElementRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TMat> GetAll<TMat>() where TMat : BasElement
        {
            return GetAll().OfType<TMat>();
        }

        public override IQueryable<BasElement> GetAll()
        {
            var elements = DbSet.Include(e => e.Fields)
                .Include(e => e.Fields.Select(f => f.FieldDefinition))
                .Include(e => e.Fields.Select(f => f.BasCode));
            return elements;
        }

        public override BasElement Get(int id)
        {
            return GetAll().SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<BasElement> GetAll(BasElementFilter filter)
        {
            var f = filter;
            IQueryable<BasElement> elements = GetAll();
            if (f.SpecialtyId != null && f.SpecialtyId != 0)
                elements = elements.Include(e => e.Type.Specialties)
                    .Where(e => e.Type.Specialties.Any(s => s.Id == f.SpecialtyId));


            elements = elements.Where(e => (f.TypeId == null || f.TypeId == 0 || f.TypeId == e.TypeId) &&
                                               (f.FullCode == null || e.FullCode.Contains(f.FullCode)));
            if (!string.IsNullOrEmpty(f.Description))
            {
                var terms = f.Description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in terms)
                {
                    var t = term;
                    elements = elements.Where(e => e.Fields.Any(fd => fd.BasCode.Description.Contains(t)))
                                        .Union(elements.Where(e => e.Observations.Contains(t)));
                }

            }

            elements = elements.OrderBy(e => e.FullCode);

            if (f.Take != null && f.Skip != null)
                elements = elements.Skip(f.Skip.Value).Take(f.Take.Value);

            return elements;
        }

        public IList<BasElementType> GetAllElementTypes()
        {
            if(_elementTypes == null)
                _elementTypes =  DbContext.Set<BasElementType>()
                                        .Include(t => t.Class)
                                        .Include(t => t.Class.ExtraAttributes)
                                        .Include(t => t.FieldDefinitions)
                                        .Include(t => t.Specialties)
                                        .ToList();
            return _elementTypes;
        }

        public override BasElement Add(BasElement element)
        {
            var result = DbSet.Add(element);
            SetFieldsUnchanged(element);
            return result;
        }

        public override void Update(BasElement element)
        {
            base.Update(element);
            foreach (var field in element.Fields)
            {
                DbContext.Entry(field).State = EntityState.Modified;
            }
            SetFieldsUnchanged(element);
        }



        public override void Delete(BasElement element)
        {
            var dbEntityEntry = DbContext.Entry(element);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(element);
            }
            SetFieldsUnchanged(element);
            DbSet.Remove(element);
        }

        private void SetFieldsUnchanged(BasElement element)
        {
            foreach (var field in element.Fields)
            {
                if(field.BasCode != null)
                    DbContext.Entry(field.BasCode).State = EntityState.Unchanged;
                if (field.FieldDefinition != null)
                    DbContext.Entry(field.FieldDefinition).State = EntityState.Unchanged;
                    
            }
        }


    }
}
