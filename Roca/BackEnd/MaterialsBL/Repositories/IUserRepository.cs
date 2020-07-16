using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IUserRepository : IRepository<int, User>
    {
        IQueryable<User> GetAllByRole(string role);
    }
}
