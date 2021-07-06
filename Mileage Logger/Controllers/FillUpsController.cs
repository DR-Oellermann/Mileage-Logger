using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mileage_Logger.IdentityManagement;
using Mileage_Logger.Models;

namespace Mileage_Logger.Controllers
{
    [Auth(Roles = ("admin, user"))]
    public class FillUpsController : Controller
    {
        private milageTrackerEntities db = new milageTrackerEntities();
        static AccountModel accountModel = new AccountModel();
        private int userID = accountModel.findUserId(UserSession.Username);

        // GET: FillUps
        public ActionResult Index()
        {
            var tblFillUps = db.tblFillUps.Where(x => x.User_ID == userID)
                .Include(t => t.tblCar).Include(t => t.tblFuelType)
                .Include(t => t.tblUser);
                
            return View(tblFillUps.ToList());
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        // GET: FillUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            if (tblFillUp == null)
            {
                return HttpNotFound();
            }
            return View(tblFillUp);
        }

        // GET: FillUps/Create
        public ActionResult Create()
        {
            ViewBag.Car_ID = new SelectList(db.tblCars.Where(x=>x.User_ID == userID), "Car_ID", "Car_Name");
            ViewBag.FuelType_ID = new SelectList(db.tblFuelTypes, "FuelType_ID", "FuelType_Description");
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username");
            return View();
        }

        // POST: FillUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FillUp_Milage,FillUp_Odo,FillUp_DateTime,FuelType_ID,Car_ID,FillUp_Liters,FillUp_LiterPrice,FillUp_Total,FillUp_SlipImage")] tblFillUp tblFillUp)
        {
            AccountModel accountModel = new AccountModel();
            if (ModelState.IsValid)
            {
                //db.tblFillUps.Add(tblFillUp);
                //tblFillUp.User_ID(accountModel.findUserId(UserSession.Username));

                //need to add image save

                db.tblFillUps.Add(new tblFillUp()
                {
                    FillUp_Milage = tblFillUp.FillUp_Milage,
                    FillUp_Odo = tblFillUp.FillUp_Odo,
                    FillUp_DateTime = tblFillUp.FillUp_DateTime.Date,
                    FuelType_ID = tblFillUp.FuelType_ID,
                    Car_ID = tblFillUp.Car_ID,
                    User_ID = accountModel.findUserId(UserSession.Username),
                    FillUp_Liters = tblFillUp.FillUp_Liters,
                    FillUp_LiterPrice = tblFillUp.FillUp_LiterPrice,
                    FillUp_Total = tblFillUp.FillUp_Total,
                    FillUp_SlipImage = tblFillUp.FillUp_SlipImage
                });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Car_ID = new SelectList(db.tblCars.Where(x => x.User_ID == userID), "Car_ID", "Car_Name");
            ViewBag.FuelType_ID = new SelectList(db.tblFuelTypes, "FuelType_ID", "FuelType_Description", tblFillUp.FuelType_ID);
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username", tblFillUp.User_ID);
            return View(tblFillUp);
        }

        // GET: FillUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            if (tblFillUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Car_ID = new SelectList(db.tblCars.Where(x => x.User_ID == userID), "Car_ID", "Car_Name");
            ViewBag.FuelType_ID = new SelectList(db.tblFuelTypes, "FuelType_ID", "FuelType_Description", tblFillUp.FuelType_ID);
            return View(tblFillUp);
        }

        // POST: FillUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FillUp_ID,FillUp_Milage,FillUp_Odo,FillUp_DateTime,FuelType_ID,Car_ID,FillUp_Liters,FillUp_LiterPrice,FillUp_Total,FillUp_SlipImage")] tblFillUp tblFillUp)
        {
            // add image edit and validation
            if (ModelState.IsValid)
            {
                var currentFillUp = db.tblFillUps.FirstOrDefault(x => x.FillUp_ID == tblFillUp.FillUp_ID);
                if (currentFillUp == null)
                    return HttpNotFound();

                currentFillUp.FillUp_DateTime = tblFillUp.FillUp_DateTime;
                currentFillUp.FillUp_LiterPrice = tblFillUp.FillUp_LiterPrice;
                currentFillUp.FillUp_Liters = tblFillUp.FillUp_Liters;
                currentFillUp.FillUp_Milage = tblFillUp.FillUp_Milage;
                currentFillUp.FillUp_Odo = tblFillUp.FillUp_Odo;
                currentFillUp.FillUp_SlipImage = tblFillUp.FillUp_SlipImage;
                currentFillUp.FuelType_ID = tblFillUp.FuelType_ID;
                currentFillUp.Car_ID = tblFillUp.Car_ID;
                currentFillUp.FillUp_Total = tblFillUp.FillUp_Total;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Car_ID = new SelectList(db.tblCars.Where(x => x.User_ID == userID), "Car_ID", "Car_Name");
            ViewBag.FuelType_ID = new SelectList(db.tblFuelTypes, "FuelType_ID", "FuelType_Description", tblFillUp.FuelType_ID);
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username", tblFillUp.User_ID);
            return View(tblFillUp);
        }

        // GET: FillUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            if (tblFillUp == null)
            {
                return HttpNotFound();
            }
            return View(tblFillUp);
        }

        // POST: FillUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            db.tblFillUps.Remove(tblFillUp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
