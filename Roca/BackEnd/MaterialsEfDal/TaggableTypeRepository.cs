using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class TaggableTypeRepository : EfGenericRepository<int, TaggableType>, ITaggableTypeRepository
    {
        public TaggableTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public TaggableType GetFull(int id)
        {
            return GetAllFull().SingleOrDefault(t => t.Id == id);
        }

        public IQueryable<TaggableType> GetAllRoot(int specialtyId)
        {
            return GetAllFull()
                .Where(t => t.SpecialtyId == specialtyId)
                .Where(t => t.ParentId == null);
        }

        private IQueryable<TaggableType> GetAllFull()
        {
            return DbSet.Include(t => t.Subtypes.Select(c => c.Attributes))
                .Include(t => t.Attributes)
                .Include(t => t.Specialty);
        }

        public override void Delete(TaggableType type)
        {
            if (type.Subtypes != null)
                type.Subtypes.Clear();
            if (type.Attributes != null)
                type.Attributes.Clear();
            base.Delete(type);
        }

        public override void Update(TaggableType type)
        {
            base.Update(type);
        }


        public TaggableAttribute AddAttribute(TaggableAttribute attribute)
        {
            DbContext.Entry(attribute).State = EntityState.Added;
            return attribute;
        }


        public void UpdateAttribute(TaggableAttribute attribute)
        {
            Update<TaggableAttribute>(attribute);
        }

        public void DeleteAttribute(TaggableAttribute attribute)
        {
            var entry = DbContext.Entry(attribute);
            entry.State = EntityState.Deleted;
        }

    }
}

