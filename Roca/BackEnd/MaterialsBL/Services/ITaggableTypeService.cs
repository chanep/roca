using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface ITaggableTypeService
    {
        TaggableType GetFull(int id);
        IEnumerable<TaggableType> GetAllRoot(int specialtyId);
        TaggableAttribute AddAttribute(TaggableAttribute attribute);
        void OverwriteAttributes(IEnumerable<TaggableAttribute> attributes);
        TaggableType AddType(TaggableType type);
        void UpdateType(TaggableType type);
        void DeleteType(TaggableType type);
    }
}