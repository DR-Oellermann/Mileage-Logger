using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Mileage_Logger.Models;
using Newtonsoft.Json.Serialization;

namespace Mileage_Logger.IdentityManagement
{
    public class IdentityPrincipal
    {
        private tblUser _userAccount;

        milageTrackerEntities db = new milageTrackerEntities();
        private tblUser TblUser = new tblUser();

        public IIdentity Identity { get; set; }

        public IdentityPrincipal(tblUser userAccount)
        {
            this._userAccount = userAccount;
            this.Identity = new GenericIdentity(userAccount.Username);
        }

        public bool IsInRole(string role)
        {
            //need to fix to take user and admin -- done splits with comma (use comma in mdoel when authorize attribute)
            var roles = role.Split(new char[] { ',' });
            return roles.Any(x => this._userAccount.IsAdmin.Contains(x));
        }

    }
}