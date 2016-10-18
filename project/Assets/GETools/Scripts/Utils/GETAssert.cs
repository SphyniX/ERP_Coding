using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GETools
{
    /// <summary>
    /// 断言工具类
    /// </summary>
    public class GETAssert
    {
        public delegate void OnFail();

        public static void isTrue(bool exp)
        {
            isTrue(exp, null, null, null);
        }
        public static void isTrue(bool exp, string msg)
        {
            isTrue(exp, null, msg, null);
        }
        public static void isTrue(bool exp, string msg, params object[] args)
        {
            isTrue(exp, null, msg, args);
        }
        public static void isTrue(bool exp, OnFail onFail, string msg)
        {
            isTrue(exp, onFail, msg, null);
        }

        public static void isTrue(bool exp, OnFail onFail, string msg, params object[] args)
        {
            if (exp) return;
            AssertFail(onFail, msg, args);
        }



        public static void notNull(object obj)
        {
            notNull(obj, null, null, null);
        }
        public static void notNull(object obj, string msg)
        {
            notNull(obj, null, msg, null);
        }
        public static void notNull(object obj, string msg, params object[] args)
        {
            notNull(obj, null, msg, args);
        }
        public static void notNull(object obj, OnFail onFail, string msg)
        {
            notNull(obj, onFail, msg, null);
        }

        public static void notNull(object obj, OnFail onFail, string msg, params object[] args)
        {
            if (obj != null) return;
            AssertFail(onFail, msg, args);
        }

        private static void AssertFail(OnFail onFail, string msg, params object[] args)
        {
            if (onFail != null)
            {
                onFail();
            }
            if (args == null)
            {
                throw new Exception(msg);
            }
            else
            {
                throw new Exception(string.Format(msg, args));
            }
        }
    }
}