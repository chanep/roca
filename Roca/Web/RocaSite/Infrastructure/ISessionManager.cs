using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public interface ISessionManager
    {
        User GetCurrentUser();
        SessionContext GetSessionContext();
        void SaveSessionContext(SessionContext sessionContext);
        void SetCurrentUser(User user);
    }
}