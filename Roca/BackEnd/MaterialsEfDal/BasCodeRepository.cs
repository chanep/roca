using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class BasCodeRepository : EfGenericRepository<int, BasCode>, IBasCodeRepository
    {
        public BasCodeRepository(DbContext dbContext) : base(dbContext)
        {
        }


    }
}
