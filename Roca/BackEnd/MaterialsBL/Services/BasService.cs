using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class BasService : BaseService, IBasService
    {
        private IBasCodeRepository _basCodes;
        private IBasElementRepository _basElements;
        private Dictionary<string, Dictionary<string, BasCode>> _allBasCodesByDesc;
        private Dictionary<string, Dictionary<string, BasCode>> _allBasCodesByCode;
        private BasElementFactory _elementFactory;

        private BasElementFactory ElementFactory
        {
            get
            {
                if (_elementFactory == null)
                    _elementFactory = new BasElementFactory(GetAllElementTypes());
                return _elementFactory;
            }
        }

        public BasService(IRocaUow rocaUow) : base(rocaUow)
        {
            _basCodes = rocaUow.BasCodes;
            _basElements = rocaUow.BasElements;
            _allBasCodesByDesc = new Dictionary<string, Dictionary<string, BasCode>>();
            _allBasCodesByCode = new Dictionary<string, Dictionary<string, BasCode>>();
        }

        public IEnumerable<BasCode> GetAllCodesByField(string field)
        {
            return _basCodes.GetAll().Where(l => l.Field == field);
        }

        public BasCode AddCode(BasCode basCode)
        {
            basCode = _basCodes.Add(basCode);
            RocaUow.Commit();
            return basCode;
        }

        public void DeleteCode(int basCodeId)
        {
            _basCodes.Delete(basCodeId);
            RocaUow.Commit();
        }

        public void UpdateCode(BasCode basCode)
        {
            if(_basCodes.GetAll().Any(b => b.Field == basCode.Field && b.Id != basCode.Id && b.Code == basCode.Code))
                throw new RocaUserException("Codigo duplicado: " + basCode.Code);
            if (_basCodes.GetAll().Any(b => b.Field == basCode.Field && b.Id != basCode.Id && b.Description == basCode.Description))
                throw new RocaUserException("Descripcion duplicada: " + basCode.Description);
            _basCodes.Update(basCode);
            RocaUow.Commit();
        }


        public string GetCodeByDesc(string field, string description)
        {
            if (!_allBasCodesByDesc.ContainsKey(field))
            {
                var list = _basCodes.GetAll().Where(l => l.Field == field);
                var fieldBasCodes = new Dictionary<string, BasCode>();
                foreach (var basCode in list)
                {
                    if (fieldBasCodes.ContainsKey(basCode.Description.Trim()))
                        throw new RocaException("Descripcion duplicada: " + basCode.Description.Trim());
                    fieldBasCodes.Add(basCode.Description.Trim(), basCode);
                }
                _allBasCodesByDesc.Add(field, fieldBasCodes);
            }

            if (!_allBasCodesByDesc[field].ContainsKey(description.Trim()))
                return null;
            return _allBasCodesByDesc[field][description.Trim()].Code;

        }

        private BasCode GetCodeByCode(string field, string code, bool cached)
        {
            if(!cached)
                return _basCodes.GetAll().FirstOrDefault(b => b.Field == field && b.Code == code);

            if (!_allBasCodesByCode.ContainsKey(field))
            {
                var list = _basCodes.GetAll().Where(l => l.Field == field);
                var fieldBasCodes = new Dictionary<string, BasCode>();
                foreach (var basCode in list)
                {
                    fieldBasCodes.Add(basCode.Code, basCode);
                }
                _allBasCodesByCode.Add(field, fieldBasCodes);
            }
            if (!_allBasCodesByCode[field].ContainsKey(code))
                return null;
            return _allBasCodesByCode[field][code];
        }



        public IList<BasElementType> GetAllElementTypes()
        {
            return _basElements.GetAllElementTypes();
        }


        public BasElement GetElement(int elementId)
        {
            return _basElements.Get(elementId);
        }

        public IEnumerable<BasElement> GetAllElements(BasElementFilter filter)
        {
            return _basElements.GetAll(filter);
        }

        public int GetAllElementsCount(BasElementFilter filter)
        {
            int? take = filter.Take;
            int? skip = filter.Skip;
            filter.Take = null;
            filter.Skip = null;
            var count =  _basElements.GetAll(filter).Count();
            filter.Take = take;
            filter.Skip = skip;
            return count;
        }

        public BasElement AddElement(BasElement element)
        {
            element = _basElements.Add(element);
            RocaUow.Commit();
            return element;
        }

        public void UpdateElement(BasElement element)
        {
            _basElements.Update(element);
            RocaUow.Commit();
        }

        public void DeleteElement(int elementId)
        {
            _basElements.Delete(elementId);
            RocaUow.Commit();
        }


        public BasElement BuildElementByCode(string fullCode, int elementTypeId)
        {
            return BuildElementByCode(fullCode, elementTypeId, false);
        }

        public BasElement BuildElementByCode(string fullCode, int elementTypeId, bool cached)
        {
            BasElement element = ElementFactory.CreateElement(elementTypeId);
            for(int i = 0; i < element.Fields.Count; i++)
            {
                var field = element.Fields.First(f => f.FieldDefinition.Order == i);
                var fieldDef = field.FieldDefinition;
                int fieldPos = element.Fields.Select(f => f.FieldDefinition).Select(f => f.Length).Take(i).Sum();
                string code = fullCode.Substring(fieldPos, fieldDef.Length);
                var basCode = GetCodeByCode(fieldDef.Code, code, cached);
                if(basCode == null)
                    throw new RocaException("No existe el codigo " + code + " de tipo " + fieldDef.Code);
                field.BasCodeId = basCode.Id;
                field.BasCode = basCode;

            }
            element.FullCode = fullCode;

            return element;
        }



    }
}
