using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.Core.Entity;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class ModelRepository : IModelRepository
    {
        public IEnumerable<Project> GetProjects()
        {
            var dal = new ProjectDal();
            return dal.GetAll().OrderBy(p => p.Id );
        }

        public IEnumerable<Area> GetAreas(string projId)
        {
            var dal = new AreaDal();
            return dal.GetAll(a => a.Id.ProjectId == projId, a => Direction.Asc(a.Name));
        }

        public IEnumerable<MaterialPiping> GetMaterials(string projId, string areaId, MaterialOptionalFields optFields, MaterialPipingOrder order)
        {
            var dal = new MaterialPipingDal();
            return dal.GetMaterials(projId, areaId, optFields, order);
        }

    }
}
