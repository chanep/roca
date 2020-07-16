using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class TaggableTypeService : BaseService, ITaggableTypeService
    {
        public TaggableTypeService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        public TaggableType GetFull(int id)
        {
            return RocaUow.TaggableTypes.GetFull(id);
        }

        public IEnumerable<TaggableType> GetAllRoot(int specialtyId)
        {
            return RocaUow.TaggableTypes.GetAllRoot(specialtyId);
        }


        public TaggableType AddType(TaggableType type)
        {
            RocaUow.TaggableTypes.Add(type);
            RocaUow.Commit();
            return type;
        }

        public void UpdateType(TaggableType type)
        {
            RocaUow.TaggableTypes.Update(type);
            RocaUow.Commit();
        }

        public void DeleteType(TaggableType type)
        {
            RocaUow.TaggableTypes.Delete(type);
            RocaUow.Commit();
        }


        public TaggableAttribute AddAttribute(TaggableAttribute attribute)
        {
            RocaUow.TaggableTypes.AddAttribute(attribute);
            RocaUow.Commit();
            return attribute;
        }


        public void OverwriteAttributes(IEnumerable<TaggableAttribute> attributes)
        {
            var attrs = attributes.ToList();
            if (attrs.Count == 0)
                return;
            var typeId = attrs[0].TypeId;
            if (typeId <= 0)
                throw new RocaException("Error al sobrescribir atributos de tipo tagueable. TypeId del atributo debe ser mayor a 0");

            var oldAttributes = RocaUow.TaggableTypes.GetFull(typeId).Attributes.ToList();
            foreach (var oldAttribute in oldAttributes)
            {
                RocaUow.TaggableTypes.DeleteAttribute(oldAttribute);
            }

            foreach (var attribute in attrs)
            {
                attribute.TypeId = typeId;
                RocaUow.TaggableTypes.AddAttribute(attribute);
            }
            RocaUow.Commit();
        }


    }
}
