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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;
using MimeKit;
using MailKit.Net.Smtp;

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
            var stripePublishKey = ConfigurationManager.AppSettings["pk_test_Hoa0Coqp2Ao1v3i1wYoo494m"];
            ViewBag.StripePublishKey = "pk_test_Hoa0Coqp2Ao1v3i1wYoo494m";
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
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: RaceSignUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,DateOfBirth,TshirtSize,RaceID,Email")] RaceSignUp raceSignUp, int? id)
        {///need to add an empty list for CreateRaces so it isnt null
            if (ModelState.IsValid)
            {

                raceSignUp.CreateRaces = new List<CreateRace>();

                var races = db.RaceSignUps.Where(c => c.ID == raceSignUp.ID).Select(p => p.CreateRaces);
                races.ToList();

                CreateRace cr = db.CreateRaces.Where(c => c.ID == id).FirstOrDefault();

                raceSignUp.CreateRaces.Add(cr); 

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
                Amount = 1300,//charge in cents
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

        public ActionResult ShowMyRacesRunning(int? id)
        {
            {
                //this logic needs to be somewhere exclusively for RaceCreators
                string userId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userId);

                //CreateRace race = db.CreateRaces.Find(id);
                //CreateRace race;
                if (currentUser.Email != null)
                {
                    var races = db.CreateRaces.Where(s => s.RaceSignUps.Any(c => c.Email == currentUser.Email));
                    //get ID from those races, then display race info
                    //select all RaceID's 
                    //need a list of entries
                    races.ToList();
                    if (races == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(races);
                    }

                }
                else
                {
                    return View(db.CreateRaces.ToList()); //shows all races 
                }
            }
        }

        [HttpGet]
        public ActionResult UploadCSV(int? id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            //query table to find correct race based on ID
            //pass in race object to view 
            return View();
        }

        [HttpPost]
        public ActionResult UploadCSV(HttpPostedFileBase file, int id)
        {

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                System.IO.StreamReader csvReader = new StreamReader(file.InputStream);
                {
                    string inputLine = "";
                    while ((inputLine = csvReader.ReadLine()) != null)
                    {
                        string[] values = inputLine.Split(new Char[] { ',' });
                        RaceSignUp raceSignUp = new RaceSignUp();
                        raceSignUp.FirstName = values[0];
                        raceSignUp.LastName = values[1];
                        raceSignUp.Gender = values[2]; 
                        raceSignUp.DateOfBirth = DateTime.Parse(values[3]);
                        raceSignUp.TshirtSize = values[4];
                        raceSignUp.Email = values[5];
                        raceSignUp.RaceID = id;

                        CreateRace cr = db.CreateRaces.Where(c => c.ID == id).FirstOrDefault();
                        raceSignUp.CreateRaces.Add(cr);

                        //CreateRace cr = new CreateRace();
                        ////need to add hashset value for this given race
                        //raceSignUp.CreateRaces = values[cr];
                        db.RaceSignUps.Add(raceSignUp);
                        db.SaveChanges();
                    }
                    csvReader.Close();
                }
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchBy, string search)
        {
            if (searchBy == "RaceName")
                return View(db.CreateRaces.Where(x => x.RaceName.StartsWith(search) || search == null).ToList());
            else if (searchBy == "RaceLocation")
                return View(db.CreateRaces.Where(x => x.RaceLocation.StartsWith(search) || search == null).ToList());
            else if (searchBy == "RaceDate")
                return View(db.CreateRaces.Where(x => x.RaceDate.ToString() == (search) || search == null).ToList());
            else if (searchBy == "RaceDistance")
                return View(db.CreateRaces.Where(x => x.RaceDistance.ToString() == (search) || search == null).ToList());
            else if (searchBy == "RaceFee")
                return View(db.CreateRaces.Where(x => x.RaceCost.ToString() == (search) || search == null).ToList());
            else
                return View(); //how to return "Search" view
        }

        [HttpGet]
        public ActionResult SendEmail(int? id)
        {
            //CreateRace createRace = db.CreateRaces.Find(id);
            //query table to find correct race based on ID
            //pass in race object to view 
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail()
        {//possibly create binds
            //CreateRace createRace = db.CreateRaces.Find(id);
            try
            {
                // Compose a message
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("Excited Admin", "foo@sandbox62c8e0df34d2444681f56c32138a7aaa.mailgun.org"));
                mail.To.Add(new MailboxAddress("Excited User", "tmisaacs19@gmail.com")); //variable storing RD email
                mail.Subject = "Hello"; //maybe store variable for mail.subject
                mail.Body = new TextPart("plain")
                {
                    Text = @"This is a reminder for the race you signed up for!", //variable storing message from text box 
                };

                // Send it!
                using (var client = new SmtpClient())
                {
                    // XXX - Should this be a little different?
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.mailgun.org", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("postmaster@sandbox62c8e0df34d2444681f56c32138a7aaa.mailgun.org", "272bea244d70b881f62d3111fe6da5c0-bdd08c82-5ccae067");

                    client.Send(mail);
                    client.Disconnect(true);
                    ViewBag.Message = "Email was sent successfully!!";

                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Email failed to send!!";
                return View();
            }
        }

    }
}
