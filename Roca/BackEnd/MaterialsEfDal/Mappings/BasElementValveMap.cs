using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class BasElementValveMap : EntityTypeConfiguration<BasElementValve>
    {
        public BasElementValveMap()
        {
            this.Map<BasElementValve>(t =>
            {
                t.Requires("BAS_CLASS_ID").HasValue(3).HasColumnType("INT");

            });
        }
    }
}
