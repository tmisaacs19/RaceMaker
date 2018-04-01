using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;




namespace RaceMaker.Models
{
    public class CreateRacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: CreateRaces
        public ActionResult Index(int? id)
        {
            //how to get the email of the current logged in user
            //how to get the email item from all listings in the Races Database
            //CreateRace createRace = db.CreateRaces.Find(id);
            //CreateRace createRace = new CreateRace();
            //LoginViewModel avm = new LoginViewModel();
            //CreateRace race = db.CreateRaces.Find(id);
            ////var currentEmail = email;
            //if (race.AdminEmail == avm.Email)
            //{

            //    var races = db.CreateRaces.Where(s => s.AdminEmail == avm.Email);
            //    races.ToList();
            //    return View(races);
            //    //return View(db.CreateRaces.ToList());
            //}
            //else
            //{
                return View(db.CreateRaces.ToList());
            //}
            //linq queery to database for whatever items searched for
            //you need some action, that passes the action into the controller that
            //indicates what we're looking for           
        }

        // GET: CreateRaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateRace createRace = db.CreateRaces.Find(id);
            if (createRace == null)
            {
                return HttpNotFound();
            }
            return View(createRace);
        }

        // GET: CreateRaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateRaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RaceName,RaceLocation,RaceDate,RaceDistance,RaceCost,RaceDescription,AdminEmail,AdminPassword")] CreateRace createRace)
        {
            
            if (ModelState.IsValid)
            {
                db.CreateRaces.Add(createRace);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = createRace.ID });
            }

            return View(createRace);
        }

        // GET: CreateRaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateRace createRace = db.CreateRaces.Find(id);
            if (createRace == null)
            {
                return HttpNotFound();
            }
            return View(createRace);
        }

        // POST: CreateRaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RaceName,RaceLocation,RaceDate,RaceDistance,RaceCost")] CreateRace createRace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createRace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createRace);
        }

        // GET: CreateRaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateRace createRace = db.CreateRaces.Find(id);
            if (createRace == null)
            {
                return HttpNotFound();
            }
            return View(createRace);
        }

        // POST: CreateRaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            db.CreateRaces.Remove(createRace);
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
