using System;
using System.Linq;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface ITaggableTypeRepository: IRepository<int, TaggableType>
    {
        IQueryable<TaggableType> GetAllRoot(int specialtyId);
        TaggableAttribute AddAttribute(TaggableAttribute attribute);
        void UpdateAttribute(TaggableAttribute attribute);
        void DeleteAttribute(TaggableAttribute attribute);
        TaggableType GetFull(int id);
    }
}