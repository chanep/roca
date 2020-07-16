using System;
using System.Web.Mvc;

namespace Cno.Roca.Web.RocaSite
{
    public class JsonDateModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {           
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
            long intDate;
            if (value.StartsWith(@"/Date("))
            {
                var strDate = value.Substring(6).Replace(")/", String.Empty);
                intDate = Int64.Parse(strDate);
                var date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(intDate).ToLocalTime();
                return date;
            }
            if (Int64.TryParse(value, out intDate))
            {
                var date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(intDate).ToLocalTime();
                return date;
            }
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                var dateStr = value.Trim('"');
                DateTime date;
                if (DateTime.TryParse(dateStr, out date))
                    return date;
            }
            return base.BindModel(controllerContext, bindingContext);
        }

    }
}