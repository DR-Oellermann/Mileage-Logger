using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Mileage_Logger.IdentityManagement
{
    public class IdentityPrincipal
    {
        private UserAccount _userAccount;

        public IIdentity Identity { get; set; }

        public IdentityPrincipal(UserAccount userAccount)
        {
            this._userAccount = userAccount;
            this.Identity = new GenericIdentity(userAccount.Username);
        }

        public bool IsInRole(string role)
        {
            //need to fix to take user and admin -- done splits with comma (use comma in mdoel when authorize attribute)
            var roles = role.Split(new char[] { ',' });
            return roles.Any(x => this._userAccount.Role.Contains(x));
        }

    }
}