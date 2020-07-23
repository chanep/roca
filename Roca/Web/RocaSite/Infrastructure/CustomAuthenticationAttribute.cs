using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using Cno.Roca.Web.RocaSite.Controllers;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerType = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType;
            var actionName = filterContext.ActionDescriptor.ActionName;
            if (controllerType == typeof (UserController) )
            {
                return;
            }

            var authProvider = DependencyResolver.Current.GetService<IAuthProvider>();

            if (!authProvider.IsRequestAuthenticated())
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                filterContext.HttpContext.Response.AppendHeader("roca-redirection", @"User/Login");
            }
        }
    }
}
