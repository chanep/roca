using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementFactory
    {
        private readonly IEnumerable<BasElementType> _elementTypes;
        public BasElementFactory(IEnumerable<BasElementType> elementTypes )
        {
            _elementTypes = elementTypes;
        }

        public BasElement CreateElement(int elementTypeId)
        {
            var elementType = _elementTypes.FirstOrDefault(e => e.Id == elementTypeId);
            if(elementType == null)
                throw new RocaException("No existe el tipo de elemento con id: " + elementTypeId);
            var element = (BasElement) Activator.CreateInstance(Type.GetType(elementType.Class.Name));
            element.TypeId = elementTypeId;
            foreach (var fieldDef in elementType.FieldDefinitions)
            {
                var field = new BasCodeField();
                //field.FieldDefinition = fieldDef.Clone();  //se arma kilombo al haber 2 entidades del mismo tipo y con el mismo Id en ObjectStateManager del DBContext
                field.FieldDefinition = fieldDef;
                field.FieldDefinitionId = fieldDef.Id;
                field.BasCode = null;
                field.BasCodeId = 0;
                element.Fields.Add(field);
            }
            return element;
        }

        public BasElement CreateElement(int elementTypeId, IEnumerable<BasClassAttributeWithValue> extraAttributes)
        {
            var element = CreateElement(elementTypeId);
            foreach (var attWV in extraAttributes)
            {
                object value = null;
                switch (attWV.Type)
                {
                    case "Double": 
                        value = Convert.ToDouble(attWV.Value.Replace(',','.'));
                        break;
                    case "String":
                        value = attWV.Value;
                        break;
                    case "Int":
                        value = Convert.ToInt32(attWV.Value);
                        break;
                    default:
                        throw new RocaException("Tipo invalido de atributo de clase: " );
                }
                element.GetType().GetProperty(attWV.Property).SetValue(element, value, null);
            }
            return element;
        }

        public IList<BasClassAttributeWithValue> GetExtraAttributesWithValue(BasElement element)
        {
            var extraAttributesWithValue = new List<BasClassAttributeWithValue>();
            var className = element.GetType().FullName;
            var elementClass = _elementTypes.Select(t => t.Class).First(c => c.Name == className);

            foreach (var attribute in elementClass.ExtraAttributes)
            {
                var attWV = new BasClassAttributeWithValue(attribute);

                object value = element.GetType().GetProperty(attWV.Property).GetValue(element, null);
                attWV.Value = value.ToString();
                extraAttributesWithValue.Add(attWV);
            }

            return extraAttributesWithValue;
        } 



    }
}
