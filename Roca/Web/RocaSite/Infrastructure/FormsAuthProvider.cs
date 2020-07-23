using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.Web.RocaSite.Log;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class FormsAuthProvider : IAuthProvider
    {
        private IRocaService RocaService { get; set; }

        public bool FormsMode
        {
            get { return true; }
        }

        public FormsAuthProvider(IRocaService rocaService)
        {
            RocaService = rocaService;
        }

        public bool Authenticate(string username, string password)
        {
            var user = RocaService.CommonService.GetUserByLongName(username);
            if (user == null)
                return false;

            //using (var pc = new PrincipalContext(ContextType.Domain, user.Domain))
            using (var pc = new PrincipalContext(ContextType.Machine))
            {
                bool isValid = pc.ValidateCredentials(user.LongUserName, password);
                //bool isValid = true;               
                if (!isValid)
                    return false;
                FormsAuthentication.SetAuthCookie(user.LongUserName, true);
            }
            return true;
        }

        public bool IsRequestAuthenticated()
        {
            //ILogger logger = DependencyResolver.Current.GetService<ILogger>();
            
            //logger.Info(string.Format("IsAuthenticated: {0},  AuthenticatedUser: {1}, AuthenticationType: {2}", identity.IsAuthenticated, identity.Name, identity.AuthenticationType));
            //var user = SessionManager.GetCurrentUser();
            //if (user == null)
            //    return false;
            //return true;
            var identity = HttpContext.Current.User.Identity;
            return identity.IsAuthenticated;
        }

        public string GetUserName()
        {
            var name = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(name))
                return null;
            return name;
        }
    }
}