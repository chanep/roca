using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    class MaterialRepository : EfGenericRepository<int, Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TMat> GetAll<TMat>() where TMat : Material
        {
            return DbSet.OfType<TMat>();
        }

        

        public EiMaterial GetFullEiMaterial(int id)
        {
            //esto no funciona por un _bug de EF5 .NET 4.0
            //return DbSet.OfType<EiMaterial>().Include(m => m.Details).SingleOrDefault(m => m.Id = id);

            ////esto hace un inner join
            //var x = DbSet.OfType<EiMaterial>()
            //    .Join(DbContext.Set<EiMaterialDetails>(), m => m.Id, d => d.Id,
            //        (m, d) => new {EiMaterial = m, Details = d});

            ////esto hace un left join
            //var y = DbSet.OfType<EiMaterial>()
            //    .GroupJoin(DbContext.Set<EiMaterialDetails>(), m => m.Id, d => d.Id,
            //        (m, d) => new {EiMaterial = m, Details = d})
            //        .SelectMany(a => a.Details.DefaultIfEmpty(), (a, d) => new{mat=a.EiMaterial, det=d} );

            ////esto hace un left join y mete el detalle dentro del eimaterial
            //var z = DbSet.OfType<EiMaterial>()
            //    .GroupJoin(DbContext.Set<EiMaterialDetails>(), m => m.Id, d => d.Id,
            //        (m, d) => new { EiMaterial = m, Details = d })
            //        .SelectMany(a => a.Details.DefaultIfEmpty(), (a, d) => new { mat = a.EiMaterial, det = d })
            //        .AsEnumerable().Select(a=>
            //        {
            //            a.mat.Details = a.det;
            //            return a.mat;
            //        }).AsQueryable();

            var mat = DbSet.OfType<EiMaterial>().SingleOrDefault(m => m.Id == id);
            if (mat == null)
                return null;
            DbContext.Entry(mat).Reference(m => m.Details).Load();
            return mat;
        }

    }
}
