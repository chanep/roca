using System.Data.Entity;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.BackEnd.Materials.EfDal.Mappings;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class RocaContext : DbContext
    {
        static RocaContext()
        {
            Database.SetInitializer<RocaContext>(null);
        }


        public RocaContext()
            : base("Name=RocaContext")
        {
            this.Database.ExecuteSqlCommand("ALTER SESSION SET NLS_COMP=LINGUISTIC");
            this.Database.ExecuteSqlCommand("ALTER SESSION SET NLS_SORT=BINARY_AI");
            //Database.Connection.Open();
            //this.Database.Connection.StateChange += (sender, args) =>
            //{
            //    Console.WriteLine("conn change state: " + args.OriginalState + " -> " + args.CurrentState);
            //};
        }


        //public DbSet<Project> Projects { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Material> Materials { get; set; }
        //public DbSet<Specialty> Specialties { get; set; }
        //public DbSet<Unit> Units { get; set; }
        //public DbSet<MaterialList> MaterialLists { get; set; }
        //public DbSet<LookUp> LookUps { get; set; }
        //public DbSet<Document> Documents { get; set; }
        //public DbSet<TimeSheet> TimeSheets { get; set; }
        //public DbSet<MatPiping> MatPipings { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new EiMaterialMap());
            modelBuilder.Configurations.Add(new EiMaterialDetailsMap());
            modelBuilder.Configurations.Add(new MaterialListMap());
            modelBuilder.Configurations.Add(new MaterialMap());
            modelBuilder.Configurations.Add(new MlItemMap());
            modelBuilder.Configurations.Add(new PMaterialMap());
            modelBuilder.Configurations.Add(new SpecialtyMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new LookUpMap());
            modelBuilder.Configurations.Add(new TaggableTypeMap());
            modelBuilder.Configurations.Add(new TaggableAttributeMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new TimeSheetMap());
            modelBuilder.Configurations.Add(new TimeSheetItemMap());
            modelBuilder.Configurations.Add(new MatPipingMap());

            modelBuilder.Configurations.Add(new BasCodeMap());
            modelBuilder.Configurations.Add(new BasClassMap());
            modelBuilder.Configurations.Add(new BasClassAttributeMap());
            modelBuilder.Configurations.Add(new BasElementTypeMap());
            modelBuilder.Configurations.Add(new BasElementMap());
            modelBuilder.Configurations.Add(new BasElementPipingMap());
            modelBuilder.Configurations.Add(new BasElementValveMap());
            modelBuilder.Configurations.Add(new BasElementEiMap());
            modelBuilder.Configurations.Add(new BasElementCableMap());

            modelBuilder.Configurations.Add(new BasFieldDefinitionMap());
            modelBuilder.Configurations.Add(new BasCodeFieldMap());
            modelBuilder.Ignore<BasClassAttributeWithValue>();
        }
    }
}
