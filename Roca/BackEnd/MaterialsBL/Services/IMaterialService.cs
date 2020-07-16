using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface IMaterialService
    {
        EiMaterial GetFullEiMaterial(int id);
        int GetEiMaterialsCount(EiMaterialFilter filter);
        IEnumerable<EiMaterial> GetEiMaterialsPaged(EiMaterialFilter filter, int skip, int take);
        int GetEiMaterialsForMlCount(int mlId, EiMaterialFilter filter);
        IEnumerable<EiMaterial> GetEiMaterialsForMlPaged(int mlId, EiMaterialFilter filter, int skip, int take);
    }
}