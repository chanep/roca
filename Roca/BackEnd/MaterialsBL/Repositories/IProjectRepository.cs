using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IProjectRepository : IRepository<int, Project>
    {
        Project GetFull(int id);
        IQueryable<Project> GetAllRoot();
    }
}
