using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    class BasElementMap : EntityTypeConfiguration<BasElement>
    {
        public BasElementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings  //en EF5 el tema de los discriminator solo anda bien con TypePerHierarchy (todas las clases en la misma tabla=
            //this.Map<BasElementGeneric>(t =>
            //{
            //    t.ToTable("BAS_ELEMENTS", "ROCA");
            //    t.Requires("BAS_CLASS_ID").HasValue(1).HasColumnType("INT");
            //});
            //this.Map<BasElementPiping>(t =>
            //{
            //    t.ToTable("BAS_ELEMENTS_PIPING", "ROCA");
            //    t.Requires("BAS_CLASS_ID").HasValue(2).HasColumnType("INT");

            //});

            this.Map<BasElement>(t =>
            {
                t.Requires("BAS_CLASS_ID").HasValue(1).HasColumnType("INT");

            });

            this.ToTable("BAS_ELEMENTS", "ROCA");
            this.Property(t => t.Id).HasColumnName("BAS_ELEMENT_ID");
            this.Property(t => t.TypeId).HasColumnName("BAS_ELEMENT_TYPE_ID");
            this.Property(t => t.FullCode).HasColumnName("FULL_CODE");
            this.Property(t => t.Unit).HasColumnName("UNIT");
            this.Property(t => t.Observations).HasColumnName("OBSERVATIONS");
            this.Property(t => t.Weight).HasColumnName("WEIGHT");

            this.HasRequired(t => t.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId);

            this.HasMany(t => t.Fields)
                .WithRequired()
                .HasForeignKey(d => d.ElementId);

        }

    }
}
