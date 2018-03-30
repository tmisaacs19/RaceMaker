using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMaker.Models;

namespace RaceMaker.Controllers
{
    public class RaceSignUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RaceSignUps
        public ActionResult Index()
        {
            return View(db.RaceSignUps.ToList());
        }

        // GET: RaceSignUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaceSignUp raceSignUp = db.RaceSignUps.Find(id);
            if (raceSignUp == null)
            {
                return HttpNotFound();
            }
            return View(raceSignUp);
        }

        // GET: RaceSignUps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RaceSignUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] RaceSignUp raceSignUp)
        {
            if (ModelState.IsValid)
            {
                db.RaceSignUps.Add(raceSignUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(raceSignUp);
        }

        // GET: RaceSignUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaceSignUp raceSignUp = db.RaceSignUps.Find(id);
            if (raceSignUp == null)
            {
                return HttpNotFound();
            }
            return View(raceSignUp);
        }

        // POST: RaceSignUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] RaceSignUp raceSignUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raceSignUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(raceSignUp);
        }

        // GET: RaceSignUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaceSignUp raceSignUp = db.RaceSignUps.Find(id);
            if (raceSignUp == null)
            {
                return HttpNotFound();
            }
            return View(raceSignUp);
        }

        // POST: RaceSignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaceSignUp raceSignUp = db.RaceSignUps.Find(id);
            db.RaceSignUps.Remove(raceSignUp);
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

        //public ActionResult SignUpForRace(int? id)
        //{

        //}
    }
}
