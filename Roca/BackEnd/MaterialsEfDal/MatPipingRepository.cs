using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class MatPipingRepository : IMatPipingRepository
    {
        protected DbContext DbContext { get; set; }

        protected IDbSet<MatPiping> DbSet { get; set; }

        public MatPipingRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<MatPiping>();
        }

        public IQueryable<MatPiping> GetAll()
        {
            return DbSet;
        }
    }
}
