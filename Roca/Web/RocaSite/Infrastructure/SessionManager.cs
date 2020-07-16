using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Newtonsoft.Json;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private ICommonService CommonService
        {
            get { return DependencyResolver.Current.GetService<IRocaService>().CommonService; }
        }

        private SessionContext CurrentSessionContext
        {
            get { return (SessionContext)HttpContext.Current.Session["SessionContext"]; }
            set { HttpContext.Current.Session["SessionContext"] = value; }
        }

        private SessionContext SessionContextCookie
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["sessionContext"];
                if (cookie == null)
                    return null;
                var sessionContext = new JavaScriptSerializer().Deserialize<SessionContext>(cookie.Value);
                return sessionContext;
            }
            set
            {
                var jsonProject = new JavaScriptSerializer().Serialize(value);
                var cookie = new HttpCookie("sessionContext", jsonProject);
                if (HttpContext.Current.Request.Cookies["sessionContext"] == null)
                    HttpContext.Current.Response.Cookies.Add(cookie);
                else
                    HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public SessionContext GetSessionContext()
        {

            if (CurrentSessionContext == null)
            {
                if (SessionContextCookie != null)
                    CurrentSessionContext = SessionContextCookie;
                else
                    CurrentSessionContext = new SessionContext();
            }
            return CurrentSessionContext;
        }

        public void SaveSessionContext(SessionContext sessionContext)
        {
            CurrentSessionContext = sessionContext;
            SessionContextCookie = sessionContext;
        }
         

        public User GetLoggedtUser()
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                var longUserName = GetWindowsUser();
                var user = CommonService.GetUserByLongName(longUserName);
                if (user == null)
                    user = GetDefaultUser();
                HttpContext.Current.Session["user"] = user;
            }
            return (User) HttpContext.Current.Session["user"];
        }

        public void ImpersonateUser(User user)
        {
            HttpContext.Current.Session["user"] = user;
        }

        private User GetDefaultUser()
        {
            return new User()
            {
                Initials = "GST",
                Name = "Guest",
                LastName = "",
                LongUserName = @"ODEBRECHT\guest",
                Mail = "",
                Roles = Roles.Read
            };
        }

        private string GetWindowsUser()
        {
            return HttpContext.Current.Request.RequestContext.HttpContext.User.Identity.Name;
        }

        
    }
}