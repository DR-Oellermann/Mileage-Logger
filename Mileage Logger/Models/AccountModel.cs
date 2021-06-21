using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Mileage_Logger.Models
{
    // add users from db to save their details
    public class AccountModel
    {
        milageTrackerEntities db = new milageTrackerEntities();

        //finds the suers with specific username
        public tblUser findUser(string username)
        {
            return db.tblUsers.FirstOrDefault(x => x.Username == username);
        }

        // returns the ID of the user
        public int findUserId(string username)
        {
            return db.tblUsers.Where(x => x.Username == username).Select(x => x.User_ID).FirstOrDefault();
        }

        //use for login to make to username and passwd match whats in db/list
        public tblUser Login(string username, string password)
        {
            return db.tblUsers.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }

}