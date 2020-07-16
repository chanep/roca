using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.CoreData
{
    public static class EnumHelper
    {
        
        public static IEnumerable<string> GetStrings<T>(this T theEnum) where T : struct, IConvertible 
        {
            if (!typeof (T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var enu = (Enum) (object)theEnum;

            return
                Enum.GetNames(typeof (T))
                    .Where(
                        name => name != "All" && (int)Enum.Parse(enu.GetType(), name) != 0 && enu.HasFlag((Enum) Enum.Parse(enu.GetType(), name)));
        }

    }
}
