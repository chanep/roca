using System.Linq;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IMatPipingRepository
    {
        IQueryable<MatPiping> GetAll();
    }
}