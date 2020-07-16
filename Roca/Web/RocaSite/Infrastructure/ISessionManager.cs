using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public interface ISessionManager
    {
        User GetLoggedtUser();
        SessionContext GetSessionContext();
        void SaveSessionContext(SessionContext sessionContext);
        void ImpersonateUser(User user);
    }
}