using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
        bool IsRequestAuthenticated();
        bool FormsMode { get; }
        string GetUserName();

    }
}
