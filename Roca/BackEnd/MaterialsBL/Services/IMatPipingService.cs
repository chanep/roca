using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface IMatPipingService
    {
        IEnumerable<MatPiping> GetAll(MatPipingFilter filter);
    }
}