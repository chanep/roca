using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.CoreData
{
    public static class SetHelper
    {
        public static bool In<T>(this T obj, params T[] values)
        {
            foreach (var value in values)
            {
                if (obj.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
