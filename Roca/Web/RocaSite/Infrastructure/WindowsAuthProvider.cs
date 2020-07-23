using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class WindowsAuthProvider : IAuthProvider
    {
        public bool FormsMode
        {
            get { return false; }
        }

        public bool Authenticate(string username, string password)
        {
            throw new InvalidOperationException("No se puede llamar a Authenticate en Authentication mode=Windows");
        }

        public bool IsRequestAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public string GetUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}