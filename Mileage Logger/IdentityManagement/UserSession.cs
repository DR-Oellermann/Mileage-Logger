using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mileage_Logger.IdentityManagement
{
    public class UserSession
    {
        private static string sessionUserName = "username";

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }

                var _session = HttpContext.Current.Session[sessionUserName];
                if (_session != null)
                {
                    return _session.ToString();
                }

                return null;
            }
            set => HttpContext.Current.Session[sessionUserName] = value;
        }
    }

}