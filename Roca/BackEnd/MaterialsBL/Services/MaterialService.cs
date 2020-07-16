using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class MaterialService: BaseService, IMaterialService
    {
        public MaterialService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        public EiMaterial GetFullEiMaterial(int id)
        {
            return RocaUow.Materials.GetFullEiMaterial(id);
        }

        public int GetEiMaterialsCount(EiMaterialFilter filter)
        {
            return GetEiMaterials(filter).Count();
        }

        public IEnumerable<EiMaterial> GetEiMaterialsPaged(EiMaterialFilter filter, int skip, int take)
        {
            var materials = GetEiMaterials(filter).OrderBy(m => m.Id).Skip(skip).Take(take).ToArray();
            return materials;
        }

        public int GetEiMaterialsForMlCount(int mlId, EiMaterialFilter filter)
        {
            return GetEiMaterialsForMl(mlId, filter).Count();
        }


        public IEnumerable<EiMaterial> GetEiMaterialsForMlPaged(int mlId, EiMaterialFilter filter, int skip, int take)
        {
            var materials = GetEiMaterialsForMl(mlId, filter).OrderBy(m => m.Id).Skip(skip).Take(take).ToArray();
            return materials;
        }

        private IQueryable<EiMaterial> GetEiMaterials(EiMaterialFilter filter)
        {
            var mats = RocaUow.Materials.GetAll<EiMaterial>()
                .Where(m => (string.IsNullOrEmpty(filter.Description) || m.Description.ToLower().Contains(filter.Description.ToLower())));
            return mats;
        }

        private IQueryable<EiMaterial> GetEiMaterialsForMl(int mlId, EiMaterialFilter filter)
        {
            var matsAlreadyIn = RocaUow.MaterialLists.GetAllItems(mlId).Select(i => i.MaterialId).ToList();
            var mats = GetEiMaterials(filter).Where(m => !matsAlreadyIn.Contains(m.Id));
            return mats;
        }
    }
}
