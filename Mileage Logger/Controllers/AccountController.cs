using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Mileage_Logger.IdentityManagement;
using Mileage_Logger.Models;

namespace Mileage_Logger.Controllers
{
    public class AccountController : Controller
    {
        // need to add create acc page (register)
        // need to add login page
        //need to add logout
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestLogin(UserAccountViewModel userAccountViewModel)
        {
            AccountModel accountModel = new AccountModel();

            //checkls all inputs are populated and makes sure the username and passowrd match
            if (string.IsNullOrEmpty(userAccountViewModel.UserAccount.Username) ||
                (string.IsNullOrEmpty(userAccountViewModel.UserAccount.Password)) ||
                accountModel.Login(userAccountViewModel.UserAccount.Username,
                    userAccountViewModel.UserAccount.Password) == null)
            {
                ViewBag.LoginError = "The details you entered are incorrect or you need to register!";
                return View("Login");
            }

            //used to create a session for the currrent user with their username
            UserSession.Username = userAccountViewModel.UserAccount.Username;
            return RedirectToAction("Index", "Home");
        }

        //used if the user does not have permission to view the page (IE not admin)
        public ActionResult Permission()
        {
            return View("Permission");
        }

        //logs the user out - sets session to null
        public ActionResult Logout()
        {
            UserSession.Username = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }

}