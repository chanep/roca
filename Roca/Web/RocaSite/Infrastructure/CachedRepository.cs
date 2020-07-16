using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class CachedRepository
    {
        public TR Get<TR>(double seconds, Func<TR> getItem)
        {
            string cacheId = getItem.Method.Name;
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem();
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds),
                    Cache.NoSlidingExpiration);
            }
            else item = (TR) obj;
            return item;
        }

        public TR Get<T, TR>(double seconds, Func<T,TR> getItem, T p)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        public TR Get<T1, T2, TR>(double seconds, Func<T1, T2, TR> getItem, T1 p1, T2 p2)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p1) + ";" + Str(p2);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p1,p2);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        public TR Get<T1, T2, T3, TR>(double seconds, Func<T1, T2, T3, TR> getItem, T1 p1, T2 p2, T3 p3)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p1) + ";" + Str(p2) + ";" + Str(p3);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p1, p2, p3);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        public TR Get<T1, T2, T3, T4, TR>(double seconds, Func<T1, T2, T3, T4, TR> getItem, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p1) + ";" + Str(p2) + ";" + Str(p3) + ";" + Str(p4);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p1, p2, p3, p4);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        public TR Get<T1, T2, T3, T4, T5, TR>(double seconds, Func<T1, T2, T3, T4, T5, TR> getItem, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p1) + ";" + Str(p2) + ";" + Str(p3) + ";" + Str(p4) + ";" + Str(p5);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p1, p2, p3, p4, p5);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        public TR Get<T1, T2, T3, T4, T5, T6, TR>(double seconds, Func<T1, T2, T3, T4, T5, T6, TR> getItem, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {
            string cacheId = getItem.Method.Name + ";" + Str(p1) + ";" + Str(p2) + ";" + Str(p3) + ";" + Str(p4) + ";" + Str(p5) + ";" + Str(p6);
            if (getItem == null) throw new ArgumentNullException("getItem");
            TR item;
            object obj = HttpRuntime.Cache.Get(cacheId);
            if (obj == null)
            {
                item = getItem(p1, p2, p3, p4, p5, p6);
                HttpContext.Current.Cache.Insert(cacheId, item, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration);
            }
            else item = (TR)obj;
            return item;
        }

        private string Str(object obj)
        {
            if (obj == null)
                return "null";
            return obj.ToString();
        }
    }
}