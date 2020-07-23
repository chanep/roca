using System.Web.Mvc;
using Cno.Roca.Web.RocaSite.Infrastructure;

namespace Cno.Roca.Web.RocaSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthenticationAttribute());
            //filters.Add(new ProjectSelectionFilterAttribute());
            filters.Add(new ActionLoggerFilterAttribute());
            filters.Add(new ExceptionHandler());
        }
    }
}