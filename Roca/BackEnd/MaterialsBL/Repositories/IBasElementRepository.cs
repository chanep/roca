using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IBasElementRepository : IRepository<int, BasElement>
    {
        IQueryable<TMat> GetAll<TMat>() where TMat : BasElement;
        IList<BasElementType> GetAllElementTypes();
        IQueryable<BasElement> GetAll(BasElementFilter filter);
    }
}
