using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mileage_Logger.Models
{
    // add users from db to save their details
    public class AccountModel
    {
        milageTrackerEntities db = new milageTrackerEntities();
        private List<UserAccount> listAccounts = new List<UserAccount>();

        public AccountModel()
        {
            //listAccounts.Add(new userAccount {Username = "admin", Password = "admin", Role = "admin"});

            //add all users and passwords to lsit
            //uses email as username now not first name
            foreach (var item in db.tblUsers)
            {
                listAccounts.Add(new UserAccount { UserID = item.User_ID, Username = item.Email, Password = item.Password, Role = item.IsAdmin });
            }

        }

        //finds the suers with specific username
        public UserAccount findUser(string username)
        {
            return listAccounts.FirstOrDefault(x => x.Username == username);
        }

        // returns the ID of the user
        public int findUserId(string username)
        {
            return listAccounts.Where(x => x.Username == username).Select(x => x.UserID).FirstOrDefault();
        }

        //use for login to make to username and passwd match whats in db/list
        public UserAccount Login(string username, string password)
        {
            return listAccounts.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }

}