using System.Data;
using System.Data.Entity;
using System.Linq;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.EfDal
{

    public class UserRepository : EfGenericRepository<int, User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override User Add(User user)
        {
            foreach (var specialty in user.Specialties)
            {
                DbContext.Entry(specialty).State = EntityState.Unchanged;
            }
            DbContext.Entry(user).State = EntityState.Added;
            return user;
        }

        public override User Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }

        public override IQueryable<User> GetAll()
        {
            return DbSet.Include(u => u.Specialties);
        }

        public IQueryable<User> GetAllByRole(string role)
        {
            return DbSet.Include(u => u.Specialties).Where(u => u.Roles.Contains(role));
        }


    }
}
