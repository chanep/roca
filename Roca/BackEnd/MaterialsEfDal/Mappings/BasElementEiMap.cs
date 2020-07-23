using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class BasElementEiMap : EntityTypeConfiguration<BasElementEi>
    {
        public BasElementEiMap()
        {
            this.Map<BasElementEi>(t =>
            {
                t.Requires("BAS_CLASS_ID").HasValue(4).HasColumnType("INT");

            });
            
        }
    }
}
