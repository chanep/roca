using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{


    public class MatPipingMap : EntityTypeConfiguration<MatPiping>
    {
        public MatPipingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("V_MAT_PIPING", "ROCA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ProjectId).HasColumnName("PROJ_ID");
            this.Property(t => t.SpecType).HasColumnName("SPEC_TYPE");
            this.Property(t => t.CommodityCode).HasColumnName("COMMODITY_CODE");
            this.Property(t => t.OepCode).HasColumnName("OEP_CODE");
            this.Property(t => t.IdentLayout).HasColumnName("IDENT_LAYOUT");
            this.Property(t => t.ShortDescription).HasColumnName("SHORT_DESC");
            this.Property(t => t.LongDescription).HasColumnName("LONG_DESC");
            this.Property(t => t.Quantity).HasColumnName("QUANTITY");
            this.Property(t => t.TotalQuantity).HasColumnName("TOTALQUANTITY");

        }
    }
}
