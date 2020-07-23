using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class ServiceStackJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            throw new NotImplementedException();
            //HttpResponseBase response = context.HttpContext.Response;
            //response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            //if (ContentEncoding != null)
            //{
            //    response.ContentEncoding = ContentEncoding;
            //}

            //if (Data != null)
            //{
            //    ServiceStack.Text.JsConfig.IncludeNullValues = true;
            //    var dataStr = ServiceStack.Text.JsonSerializer.SerializeToString(Data);
            //    response.Write(dataStr);
            //}
        }
    }
}