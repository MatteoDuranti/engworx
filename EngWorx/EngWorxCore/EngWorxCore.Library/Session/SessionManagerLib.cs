using System.Web;
using System.Collections.Generic;

namespace EngWorxCore.Library.Session
{
    public sealed class SessionManagerLib
    {
        private static MySessionLib getSession()
        {
            if ((HttpContext.Current != null))
            {
                if (((HttpContext.Current.Session["mysessionlib"] != null)))
                {
                    return (MySessionLib)HttpContext.Current.Session["mysessionlib"];
                }
                else
                {
                    return new MySessionLib();
                }
            }
            else
            {
                return new MySessionLib();
            }
        }

        private static void writeSession(MySessionLib session)
        {
            if ((HttpContext.Current != null))
            {
                HttpContext.Current.Session.Add("mysessionlib", session);
            }
        }

        //public static void cleanSession()
        //{
        //    if ((HttpContext.Current != null))
        //    {
        //        if (((HttpContext.Current.Session["mysessionlib"] != null)))
        //        {
        //            HttpContext.Current.Session.Clear();
        //        }
        //    }
        //}

        public static List<BreadCrumb> getBreadCrumb()
        {
            MySessionLib objSession = SessionManagerLib.getSession();
            return objSession.BreadCrumb;
        }
        public static void setBreadCrumb(List<BreadCrumb> lstBreadCrumb)
        {
            MySessionLib objSession = SessionManagerLib.getSession();
            objSession.BreadCrumb = lstBreadCrumb;
            SessionManagerLib.writeSession(objSession);
        }


    }
}
