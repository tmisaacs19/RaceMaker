using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMaker.Models;
using System.Configuration;
using Stripe;


namespace RaceMaker.Controllers
{
    public class RaceSignUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.CreateRaces.ToList());
        }

        // GET: RaceSignUps
        public ActionResult PaymentConfirmation()
        {
            var stripePublishKey = ConfigurationManager.AppSettings["Publish Key"];
            ViewBag.StripePublishKey = "Publish Key";
            return View();
            //return View(db.RaceSignUps.ToList()); uncomment this part after testing the Stripe API
            //var races = from r in db.CreateRaces
            //            orderby r.RaceDate ascending
            //            select r;
            //return View(races);
            

            //where for only race director races

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
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,DateOfBirth,TshirtSize,RaceID")] RaceSignUp raceSignUp)
        {
            if (ModelState.IsValid)
            {
                // take in a race id and assign it to a participant who registered for that race id
                db.RaceSignUps.Add(raceSignUp);
                db.SaveChanges();
                return RedirectToAction("PaymentConfirmation");
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

        public ActionResult OrderByDate()
        {
            var races = from r in db.CreateRaces
                        orderby r.RaceDate ascending
                        select r;
            return View(races);
        }

        //public ActionResult SignUpForRace(int? id)
        //{

        //}

        public ActionResult OrderByDay()
        {
            var races = (from c in db.CreateRaces
                    .OrderByDescending(c => c.RaceDate)
                         select c);
            return View(races.ToList());
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = 500,//charge in cents
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            // further application specific code goes here

            return View();
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }
    }
}
