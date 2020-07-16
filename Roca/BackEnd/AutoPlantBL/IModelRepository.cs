using System.Collections.Generic;
using Cno.Roca.BackEnd.AutoPlant.Data;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public interface IModelRepository
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Area> GetAreas(string projId);
        IEnumerable<MaterialPiping> GetMaterials(string projId, string areaId, MaterialOptionalFields optFields, MaterialPipingOrder order); 
    }
}
