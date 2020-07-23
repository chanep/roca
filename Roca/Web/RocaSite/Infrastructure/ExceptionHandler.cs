using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Log;
using Cno.Roca.Web.RocaSite.Models;


namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class ExceptionHandler : IExceptionFilter
    {
        private ILogger _logger = DependencyResolver.Current.GetService<ILogger>();

        public void OnException(ExceptionContext filterContext)
        {

            if (IsInPageErrorException(filterContext))
            {
                HandleInPageErrorException(filterContext);
            }
            else
            {
                bool detailedError = ShouldDetailError(filterContext);

                var action = "SimpleError";
                if (detailedError)
                    action = "DetailedError";

                int errorId = Math.Abs(Guid.NewGuid().GetHashCode());

                string msg;
                if (detailedError)
                    msg = GetDetailedError(filterContext);
                else
                    msg = GetSimpleError(filterContext);

                _logger.Error("ErrorId: {0} - {1}", errorId, GetDetailedError(filterContext));

                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.AppendHeader("roca-redirection", @"Error/" + action);

                if (!filterContext.Controller.TempData.ContainsKey("model"))
                    filterContext.Controller.TempData.Add("model", new ErrorVm() { ErrorId = errorId, Message = msg });

                filterContext.ExceptionHandled = true;
            }


            
        }

        private bool ShouldDetailError(ExceptionContext filterContext)
        {
            var user = DependencyResolver.Current.GetService<ISessionManager>().GetCurrentUser();
            if (user != null && user.IsInRole(Roles.SuperAdmin))
                return true;
            return false;
        }

        private bool IsUserException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is RocaUserException)
                return true;
            return false;
        }

        private bool IsInPageErrorException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is InPageErrorException)
                return true;
            return false;
        }

        private void HandleInPageErrorException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;

            var msg = new StringBuilder();

            msg.AppendLine(ex.Message);
            msg.AppendLine();

            msg.AppendLine(GetUserControllerAction(filterContext));
            msg.AppendLine();

            msg.AppendFormat("Request Body: {0}", GetRequestBody(filterContext));
            msg.AppendLine();
            msg.AppendLine();

            _logger.Error(msg.ToString());

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            filterContext.ExceptionHandled = true;
            var result = new JsonResult {Data = new {Error = ex.Message}};
            filterContext.Result = result;
        }

        private string GetSimpleError(ExceptionContext filterContext)
        {
            if (IsUserException(filterContext))
                return filterContext.Exception.Message;
            return "Se ha producido un error.";
        }

        private string GetDetailedError(ExceptionContext filterContext)
        {
            var msg = new StringBuilder();

            msg.AppendLine(filterContext.Exception.Message);
            msg.AppendLine();
            
            msg.AppendLine(GetUserControllerAction(filterContext));

            msg.AppendLine("Parameters:");
            var form = filterContext.HttpContext.Request.Form;
            foreach (var key in form.AllKeys)
            {
                msg.AppendFormat("{0}: {1}{2}", key, form.Get(key), Environment.NewLine);
            }

            var queryString = filterContext.HttpContext.Request.QueryString;
            foreach (var key in queryString.AllKeys)
            {
                msg.AppendFormat("{0}: {1}{2}", key, queryString.Get(key), Environment.NewLine);
            }

            
            msg.AppendFormat("Request Body: {0}", GetRequestBody(filterContext), Environment.NewLine);
            msg.AppendLine();

            msg.AppendLine();
            msg.AppendLine(filterContext.Exception.ToString());

            return msg.ToString();           
        }


        private string GetRequestBody(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Request.InputStream.Seek(0, 0);
            var reader = new StreamReader(filterContext.HttpContext.Request.InputStream);
            string inputString = reader.ReadToEnd();
            filterContext.HttpContext.Request.InputStream.Seek(0, 0);
            return inputString;
        }

        private string GetUserControllerAction(ExceptionContext filterContext)
        {
            var userName = DependencyResolver.Current.GetService<ISessionManager>().GetCurrentUser().UserName;
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            var str = string.Format("User: {0} / Controller: {1} / Action: {2}", userName, controllerName, actionName);
            return str;
        }
    }
}