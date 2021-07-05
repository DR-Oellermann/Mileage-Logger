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
    public class CarsController : Controller
    {
        private milageTrackerEntities db = new milageTrackerEntities();
        static AccountModel accountModel = new AccountModel();
        private int userID = accountModel.findUserId(UserSession.Username);

        // GET: Cars
        public ActionResult Index()
        {
            var tblCars = db.tblCars.Include(t => t.tblUser).Where(x=>x.User_ID == userID);
            return View(tblCars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCar tblCar = db.tblCars.Find(id);
            if (tblCar == null)
            {
                return HttpNotFound();
            }
            return View(tblCar);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Car_ID,Car_Name,Car_Make,Car_Model,Car_NumberPlate,User_ID")] tblCar tblCar)
        {
            if (ModelState.IsValid)
            {
                db.tblCars.Add(tblCar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username", tblCar.User_ID);
            return View(tblCar);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCar tblCar = db.tblCars.Find(id);
            if (tblCar == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username", tblCar.User_ID);
            return View(tblCar);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Car_ID,Car_Name,Car_Make,Car_Model,Car_NumberPlate,User_ID")] tblCar tblCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.tblUsers, "User_ID", "Username", tblCar.User_ID);
            return View(tblCar);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCar tblCar = db.tblCars.Find(id);
            if (tblCar == null)
            {
                return HttpNotFound();
            }
            return View(tblCar);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCar tblCar = db.tblCars.Find(id);
            db.tblCars.Remove(tblCar);
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
