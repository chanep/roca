using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface IBasService
    {
        string GetCodeByDesc(string field, string description);
        IEnumerable<BasCode> GetAllCodesByField(string field);
        BasCode AddCode(BasCode basCode);
        void UpdateCode(BasCode basCode);
        BasElement BuildElementByCode(string fullCode, int elementTypeId);
        BasElement BuildElementByCode(string fullCode, int elementTypeId, bool cached);
        void DeleteCode(int basCodeId);
        IList<BasElementType> GetAllElementTypes();
        BasElement GetElement(int elementId);
        BasElement AddElement(BasElement element);
        void UpdateElement(BasElement element);
        IEnumerable<BasElement> GetAllElements(BasElementFilter filter);
        int GetAllElementsCount(BasElementFilter filter);
        void DeleteElement(int elementId);
    }
}