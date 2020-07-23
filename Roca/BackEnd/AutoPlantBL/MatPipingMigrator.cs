using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class MatPipingMigrator : IMatPipingMigrator
    {
        private const int BATCH_SIZE = 200;

        public int MigrateMaterials(string projectId)
        {
            int cantTotal = 0;
            int cant = 0;
            int skip = 0;
            int take = BATCH_SIZE;
            var dal = new MatPipingMigrationDal();
            

            int deleted = dal.DeleteAllDestMaterial(projectId);
            Debug.WriteLine("borrados: " + deleted);

            do
            {
                var mats = dal.GetSourceMaterials(projectId, skip, take);
                cant = mats.Count;
                cantTotal += cant;
                foreach (var mat in mats)
                {
                    dal.CreateDestMaterial(mat);
                }
                skip += take;

            } while (cant > 0);

            return cantTotal;
        }

        
    }
}
