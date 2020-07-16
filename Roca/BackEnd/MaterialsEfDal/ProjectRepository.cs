using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    public class ProjectRepository : EfGenericRepository<int, Project>, IProjectRepository
    {
        public ProjectRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public Project GetFull(int id)
        {
            return GetAllFull().SingleOrDefault(t => t.Id == id);
        }

        public IQueryable<Project> GetAllRoot()
        {
            return GetAllFull()
                .Where(p => p.ParentId == null);
        }

        private IQueryable<Project> GetAllFull()
        {
            return DbSet.Include(t => t.Subprojects.Select(sp => sp.Parent));
        }

    }
}
