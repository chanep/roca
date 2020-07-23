using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElement : Entity<int>
    {
        private ICollection<BasCodeField> _fields;

        public int TypeId { get; set; }
        public string FullCode { get; set; }
        public string Unit { get; set; }
        public string Observations { get; set; }
        public double Weight { get; set; }

        public BasElementType Type { get; set; }

        public virtual ICollection<BasCodeField> Fields
        {
            get
            {
                _fields = _fields.OrderBy(f => f.FieldDefinition.Order).ToList();
                return _fields;
            }
            set { _fields = value; }
        }

        public BasElement()
        {
            _fields = new List<BasCodeField>();
        }

        protected virtual string GetFieldDescription(BasCodeField field)
        {
            return field.BasCode.Description + ", ";
        }

        protected virtual string GetFieldShortDescription(BasCodeField field)
        {
            return field.BasCode.ShortDescription + ", ";
        }

        private void RemoveTrailingSeparator(StringBuilder sb)
        {
            if (sb.Length >= 2)
                sb.Remove(sb.Length - 2, 2);
        }

        protected string ExtractDim(string dimUnit)
        {
            if (dimUnit == "-" || dimUnit == "")
                return "";
            dimUnit = dimUnit.Replace('[', '(');
            dimUnit = dimUnit.Replace(']', ')');
            var i = dimUnit.IndexOf('(');
            if (i == -1)
                return dimUnit;
            return dimUnit.Substring(0, i).Trim();   
        }

        protected string ExtractUnit(string dimUnit)
        {
            if (dimUnit == "-" || dimUnit == "")
                return "";
            dimUnit = dimUnit.Replace('[', '(');
            dimUnit = dimUnit.Replace(']', ')');
            var i = dimUnit.IndexOf('(');
            if (i == -1)
                return "";
            var j = dimUnit.IndexOf(')');
            return dimUnit.Substring(i + 1, j - i - 1);
        }

        protected virtual string GetDimUnit(int index)
        {
            var fields = Fields.ToList();
            int i = 0;
            for (int j = 0; j < fields.Count; j++)
            {
                var field = fields[j];
                if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                {
                    if (index == i)
                    {
                        return field.BasCode.Description;
                    }
                    i++;
                }
            }
            return "";
        }

        protected virtual string GetDim(int index)
        {
            return ExtractDim(GetDimUnit(index));
        }

        protected virtual string GetUnit(int index)
        {
            return ExtractUnit(GetDimUnit(index));
        }

        public virtual string GetFullCodeFromFields()
        {
            var sb = new StringBuilder();
            foreach (var field in Fields)
            {
                if (field.BasCode == null)
                    return null;
                sb.Append(field.BasCode.Code);
            }
            return sb.ToString();
        }

        public virtual string FamilyCode
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    if (field.FieldDefinition.Type == BasFieldType.Family)
                        sb.Append(field.BasCode.Code);
                }
                return sb.ToString();
            }
        }

        public virtual string DimensionalCode
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                        sb.Append(field.BasCode.Code);
                }
                return sb.ToString();
            }
        }

        public virtual string ShortFamilyDescription
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    if (field.FieldDefinition.Type == BasFieldType.Family)
                        sb.Append(GetFieldShortDescription(field));
                }
                RemoveTrailingSeparator(sb);
                return sb.ToString();
            }
        }

        public virtual string ShortDescription
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                     sb.Append(GetFieldShortDescription(field));
                }
                RemoveTrailingSeparator(sb);
                return sb.ToString();
            }
        }

        public virtual string Description
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    sb.Append(GetFieldDescription(field));
                }
                RemoveTrailingSeparator(sb);
                return sb.ToString();
            }
        }

        public virtual string FamilyDescription
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    if (field.FieldDefinition.Type == BasFieldType.Family)
                        sb.Append(GetFieldDescription(field));
                }
                RemoveTrailingSeparator(sb);
                return sb.ToString();
            }
        }

        public virtual string DimensionalDescription
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in Fields)
                {
                    if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                        sb.Append(GetFieldDescription(field));
                }
                RemoveTrailingSeparator(sb);
                return sb.ToString();
            }
        }

        public virtual void Validate()
        {
            if (GetFullCodeFromFields() != null && FullCode != GetFullCodeFromFields())
                throw new RocaException(
                    string.Format("El codigo del elemento es {0} mientras que el codigo que forman sus campos es {1}",
                        FullCode, GetFullCodeFromFields()));
            if (Unit == null) 
                throw new RocaException("La unidad del elemento no puede ser null");
            if(TypeId == 0)
                throw new RocaException("EL TypeId del elemento no pede ser 0");
        }

        public virtual SisepcElement CreateSisepcElement()
        {
            return new SisepcElement()
            {
                Code = FullCode,
                Dim1 = GetDim(0),
                Unit1 = GetUnit(0),
                Dim2 = GetDim(1),
                Unit2 = GetUnit(1),
                Dim3 = GetDim(2),
                Unit3 = GetUnit(2),
                PurchaseUnit = Unit,
                Weight = Math.Round(Weight, 8),
                Description = Description,
                ShortDescription = ShortDescription
            };
        }

    }

}

