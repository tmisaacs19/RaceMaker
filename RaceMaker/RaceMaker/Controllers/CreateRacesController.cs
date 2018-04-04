using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RaceMaker.Models;
using System.IO;
using RestSharp;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;


namespace RaceMaker.Models
{
    public class CreateRacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: CreateRaces
        public ActionResult Index(int? id)
        {
            return View(db.CreateRaces.ToList()); //shows all races  
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
        public ActionResult Edit([Bind(Include = "ID,RaceName,RaceLocation,RaceDate,RaceDistance,RaceCost,FilePath,FileName")] CreateRace createRace)
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

        public ActionResult ShowMyRaces(int? id)
        {
            //this logic needs to be somewhere exclusively for RaceCreators
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userId);

            //CreateRace race = db.CreateRaces.Find(id);
            //CreateRace race;
            if (currentUser.Email != null)
            {

                var races = db.CreateRaces.Where(s => s.AdminEmail == currentUser.Email);
                races.ToList();
                if(races == null)
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

        public ActionResult RedirectToSignUp()
        {
            RaceSignUp raceSignUp = new RaceSignUp();
            return View("../RaceSignUps/Create", raceSignUp);
        }

        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    string path = Server.MapPath("~/Files/" + file.FileName);
        //    file.SaveAs(path);
        //    ViewBag.path = path;
        //    return View();
        //}

        [HttpGet]
        public ActionResult UploadFile(int? id)
            {
            CreateRace createRace = db.CreateRaces.Find(id);
            //query table to find correct race based on ID
            //pass in race object to view 
                return View(createRace);
            }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int? id)
        {//possibly create binds
            CreateRace createRace = db.CreateRaces.Find(id);
            try
            {
                if (file.ContentLength > 0)
                {
                    
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Files"), _FileName);
                    createRace.FileName = _FileName;
                    createRace.FilePath = _path;
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";

                return View(createRace);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        public ActionResult Download()
        {
            string path = Server.MapPath("~/Files/");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles("*.*");

            return View(files);
        }

        //public ActionResult DownloadFile(string filename)
        //{
        //    if (filename != null)
        //    {
        //        string fullPath = Path.Combine(Server.MapPath("~/Files/"), filename);
        //        return File(fullPath, "Files/");
        //    }
        //    //else
        //    //    return new HttpNotFoundResult; 
        //}

        public ActionResult DisplayEntries(int? id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            //RaceSignUp raceSignUp = db.RaceSignUps.Find(id);
            //for each db entry in RaceSignUps WHERE RaceID = CreateRace.ID
            var entries = db.RaceSignUps.Where(s => s.RaceID == createRace.ID);
            return View(entries.ToList());
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
                mail.To.Add(new MailboxAddress("Excited User", "ekim10203@gmail.com"));
                mail.Subject = "Hello";
                mail.Body = new TextPart("plain")
                {
                    Text = @"Testing 2!",
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
                    ViewBag.Message = "Email was sent Successfully!!";

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
