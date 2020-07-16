using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cno.Roca.Web.RocaSite.Infrastructure;
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

            DbDataGenerator.DeleteTestData();
            DbDataGenerator.CreateTestData();

        }

        protected void Application_End()
        {
            DbDataGenerator.DeleteTestData();
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            //Response.AddHeader("Pragma", "no-cache"); // HTTP 1.0.
            //Response.AddHeader("Expires", "0"); // Proxies.
        }
    }
}