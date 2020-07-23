using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public interface IMatPipingMigrator
    {
        int MigrateMaterials(string projectId);
    }
}
