using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Cno.Roca.BackEnd.AutoPlant.BL;
using Cno.Roca.BackEnd.AutoPlant.Data;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class CachedModelRepository : CachedRepository, IModelRepository
    {
        readonly IModelRepository _repository = new ModelRepository();

        private TR Get<TR>(string cacheId, double seconds, Func<TR> getItem) where TR : class
        {
            TR item = HttpRuntime.Cache.Get(cacheId) as TR;
            if (item == null)
            {
                item = getItem();
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            return item;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _repository.GetProjects();
        }

        public IEnumerable<Area> GetAreas(string projId)
        {
            return _repository.GetAreas(projId);
        }

        public IEnumerable<MaterialPiping> GetMaterials(string projId, string areaId, MaterialOptionalFields optFields, MaterialPipingOrder order)
        {
            return Get(30, _repository.GetMaterials, projId, areaId, optFields, order);
        }

    }
}