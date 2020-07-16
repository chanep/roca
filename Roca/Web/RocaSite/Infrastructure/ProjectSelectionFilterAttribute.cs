using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.Web.RocaSite.Controllers;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class ProjectSelectionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerType = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType;
            if (controllerType != typeof(LoginController) && 
                controllerType != typeof(ErrorController) && 
                controllerType != typeof(ProjectController) &&
                controllerType != typeof(AutoplantController) &&
                controllerType != typeof(TimeSheetController) &&
                controllerType != typeof(TaggableController))
            {
                var sessionManager = DependencyResolver.Current.GetService<ISessionManager>();
                if (sessionManager.GetSessionContext().Project == null)
                {
                    //filterContext.Result = new RedirectToRouteResult(
                    //new RouteValueDictionary 
                    //{ 
                    //    { "controller", "Project" }, 
                    //    { "action", "Selection" } 
                    //});
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.HttpContext.Response.AppendHeader("roca-redirection", @"Project/Selection");
                }
            }
        }

    }
}