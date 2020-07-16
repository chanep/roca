using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class EiMaterialDetailsMap : EntityTypeConfiguration<EiMaterialDetails>
    {
        public EiMaterialDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.LongDescription)
                .IsRequired()
                .HasMaxLength(2000);


            // Table & Column Mappings
            this.ToTable("EIMATERIAL_DETAILS", "ROCA");
            this.Property(t => t.Id).HasColumnName("MATERIAL_ID");
            this.Property(t => t.LongDescription).HasColumnName("LONG_DESCRIPTION");
        }
    }
}
