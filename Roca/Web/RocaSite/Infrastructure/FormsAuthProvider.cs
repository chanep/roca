using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.BL.Services;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class FormsAuthProvider : IAuthProvider
    {
        private IRocaService RocaService { get; set; }
        private ISessionManager SessionManager { get; set; }

        protected FormsAuthProvider(IRocaService rocaService, ISessionManager sessionManager)
        {
            RocaService = rocaService;
            SessionManager = sessionManager;
        }

        public bool Authenticate(string username, string password)
        {
            var user = RocaService.CommonService.GetUserByLongName(username);
            if (user == null)
                return false;
            using (var pc = new PrincipalContext(ContextType.Domain, user.Domain))
            {
                bool isValid = pc.ValidateCredentials(user.UserName, password);
                if (!isValid)
                    return false;
            }

            return true;
        }
    }
}