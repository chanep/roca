using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Cno.Roca.Web.RocaSite.Log;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class ActionLoggerFilterAttribute : ActionFilterAttribute
    {
        private ILogger _logger = DependencyResolver.Current.GetService<ILogger>();


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof (HttpPostAttribute), true);
            if (attributes.Length > 0 && filterContext.Exception == null)
            {
                LogAction(filterContext);
            }
        }

        private void LogAction(ActionExecutedContext context)
        {
            var user = DependencyResolver.Current.GetService<ISessionManager>().GetCurrentUser();

            var userName = "NotLogged";
            if(user != null)
                userName = user.UserName;
            var controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = context.ActionDescriptor.ActionName;

            //StringBuilder parameters = new StringBuilder();
            //foreach (var p in context.ActionDescriptor.GetParameters())
            //{
            //    if (context.Controller.ValueProvider.GetValue(p.ParameterName) != null)
            //    {
            //        parameters.AppendFormat("\r\n\t{0}\t\t:{1}", p.ParameterName,
            //                                context.Controller.ValueProvider.GetValue(p.ParameterName).AttemptedValue);
            //    }

            //}

            

            var msg = new StringBuilder();
            msg.AppendFormat("User: {0} / Controller: {1} / Action: {2}{3}",userName, controllerName, actionName, Environment.NewLine);
            msg.AppendLine("Form Parameters:");

            var form = context.HttpContext.Request.Form;
            foreach (var key in form.AllKeys)
            {
                msg.AppendFormat("{0}: {1}{2}", key, form.Get(key), Environment.NewLine);
            }
            msg.AppendLine();


            if (actionName != "Authenticate")
            {
                context.HttpContext.Request.InputStream.Seek(0, 0);
                var reader = new StreamReader(context.HttpContext.Request.InputStream);
                var inputString = reader.ReadToEnd();
                context.HttpContext.Request.InputStream.Seek(0, 0);
                msg.AppendFormat("Request Body: {0}", inputString, Environment.NewLine);
                msg.AppendLine();
            }
            _logger.Info(msg.ToString());
        }
    }
}