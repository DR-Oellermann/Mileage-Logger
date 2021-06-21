using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mileage_Logger.Models;

namespace Mileage_Logger.IdentityManagement
{
    public class AuthAttribute : AuthorizeAttribute
    {
        //overides the default authorization
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //checks if user is logged in and has a session. if not bounce them to login page
            if (string.IsNullOrEmpty(UserSession.Username))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else
            {
                AccountModel accountModel = new AccountModel();
                var user = accountModel.findUser(UserSession.Username);
                IdentityPrincipal identityPrincipal = new IdentityPrincipal(user);
                //checks if user has permission to view page - if not bounce to no permission view. Should only happen for admin page
                if (!identityPrincipal.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Permission" }));
                }
            }
        }


    }
}