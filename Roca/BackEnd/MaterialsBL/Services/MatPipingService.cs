using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class MatPipingService : BaseService, IMatPipingService
    {
        public MatPipingService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        public IEnumerable<MatPiping> GetAll(MatPipingFilter filter)
        {
            var f = filter;
            return RocaUow.MatPipings.GetAll()
                .Where(m => (f.ProjectId == null || f.ProjectId == "" || m.ProjectId == f.ProjectId) &&
                            (f.CommodityCode == null || f.CommodityCode == "" || m.CommodityCode.ToLower().Contains(f.CommodityCode)) &&
                            (f.LongDescription == null || f.LongDescription == "" || m.LongDescription.ToLower().Contains(f.LongDescription))
                );
        }
    }
}
