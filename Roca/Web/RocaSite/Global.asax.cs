using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Log;
using RocaSite;

namespace Cno.Roca.Web.RocaSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(DateTime), new JsonDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new JsonDateModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<System.Web.Mvc.JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new LargeJsonValueProviderFactory());
            //ValueProviderFactories.Factories.Add(new JsonServiceStackValueProviderFactory()); //empezo a romper las bolas con comprar una licencia
            //DbDataGenerator.DeleteTestData();
            //DbDataGenerator.CreateTestData();

        }

        protected void Application_End()
        {
            //DbDataGenerator.DeleteTestData();
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            //Response.AddHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AddHeader("Expires", "0"); // Proxies.
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ILogger logger = DependencyResolver.Current.GetService<ILogger>();

            HttpContext ctx = HttpContext.Current;


            StringBuilder sb = new StringBuilder();
            sb.Append(ctx.Request.Url.ToString() + System.Environment.NewLine);
            sb.Append("Source:" + System.Environment.NewLine + ctx.Server.GetLastError().Source.ToString());
            sb.Append("Message:" + System.Environment.NewLine + ctx.Server.GetLastError().Message.ToString());
            sb.Append("Stack Trace:" + System.Environment.NewLine + ctx.Server.GetLastError().StackTrace.ToString());
            
            logger.Error(sb.ToString());
        }
    }
}