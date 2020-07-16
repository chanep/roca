using System.Linq;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IMaterialRepository : IRepository<int, Material>
    {
        IQueryable<TMat> GetAll<TMat>() where TMat : Material;

        EiMaterial GetFullEiMaterial(int id);
    }
}