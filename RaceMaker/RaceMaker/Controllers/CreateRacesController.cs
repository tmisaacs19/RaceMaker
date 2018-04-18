﻿using System;
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using OfficeOpenXml;

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
      {//find all race points with the same Primary Key after creating JunctionTable
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateRace createRace = db.CreateRaces.Find(id);
            var raceDataPoints = db.RaceCoursePoints.Where(x => x.CreateRace.ID == id).OrderBy(a => a.PointNumber).ToList();
            createRace.RaceCoursePoints = raceDataPoints;
            if (createRace == null)
            {
                return HttpNotFound();
            }
            return View(createRace);
        }

        public ActionResult RunnerDetails(int? id)
      {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateRace createRace = db.CreateRaces.Find(id);

            var raceDataPoints = db.RaceCoursePoints.Where(x => x.CreateRace.ID == id).OrderBy(a => a.PointNumber).ToList();
            createRace.RaceCoursePoints = raceDataPoints; 

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
                createRace.RaceSignUps = new List<RaceSignUp>();

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
        public ActionResult Edit([Bind(Include = "ID,RaceName,RaceLocation,RaceDate,RaceDistance,RaceCost,FilePath,FileName,AdditionalInformation")] CreateRace createRace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createRace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = createRace.ID });
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
            return RedirectToAction("ShowMyRaces");
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
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            try
            {
                if (file.ContentLength > 0)
                {
                    
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Files"), _FileName);
                    createRace.FileName = _FileName;
                    createRace.FilePath = _path;
                    db.SaveChanges();
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

        [HttpGet]
        public ActionResult UploadImage(int? id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            //query table to find correct race based on ID
            //pass in race object to view 
            return View(createRace);
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int? id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            try
            {
                if (file.ContentLength > 0)
                {

                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    createRace.ImageName = _FileName;
                    createRace.ImagePath = _path;
                    db.SaveChanges();
                    file.SaveAs(_path);
                }
                ViewBag.Message = "Image Uploaded Successfully!!";

                return View(createRace);
            }
            catch
            {
                ViewBag.Message = "Image upload failed!!";
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


        public ActionResult DisplayEntries(int? id)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            var entries = db.RaceSignUps.Where(s => s.RaceID == createRace.ID).ToList();
            return View(entries);
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
                mail.To.Add(new MailboxAddress("Excited User", "tmisaacs19@gmail.com"));
                mail.Subject = "Hello";
                mail.Body = new TextPart("plain")
                {
                    Text = @"This is a reminder for the race you signed up for!",
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

        public ActionResult ExportDataFromDatabase(int? id)
        {
            SqlConnection Con = new SqlConnection();
            string Path = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Con.ConnectionString = Path;
            DataTable dtNew = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter("select * from RaceSignUps", Con);
            Adp.Fill(dtNew);
            CreateRace createRace = new CreateRace();

            if(dtNew.Rows.Count > 0)
            {
                
                string filePath = Server.MapPath("~/Files/ExcelExport" + createRace.RaceName + ".csv");
                
                FileInfo Files = new FileInfo(filePath);
                ExcelPackage excel = new ExcelPackage(Files);
                var sheetCreate = excel.Workbook.Worksheets.Add("RaceData");
                for(int i = 0; i < dtNew.Columns.Count; i++)
                {
                    sheetCreate.Cells[1, i + 1].Value = dtNew.Columns[i].ColumnName.ToString();
                }
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    for(int j = 0; j < dtNew.Columns.Count; j++)
                    sheetCreate.Cells[i + 2,j + 1].Value = dtNew.Rows[i][j].ToString();
                }
                excel.Save();
            }
            return View();

        }

        public ActionResult SaveMap(int? id, double centerLat, double centerLong)
        { //get map details to save
            //need save WayPoints function, then pass the data here
            //save data as variables not hard coded
            //store data to array
            //add data category to CreateRaces Model
            //display data in RunnerDetailsView
            CreateRace createRace = db.CreateRaces.Find(id);
            //fields on CR
            createRace.CenterLat = centerLat;
            createRace.CenterLong = centerLong;

            db.SaveChanges();
            return View("Details", createRace);
        }

        public void SaveRaceCoursePoints(int? id, double lat, double lng)
        {
            CreateRace createRace = db.CreateRaces.Find(id);
            RaceCoursePoint rcp = new RaceCoursePoint();
            
            //RaceCoursePoint searchRCP = db.RaceCoursePoints.Find(id);
            //get lat and longs, add to array one at a time
            //how to add points to an array asp.net mvc5 

            //rcp.ID = createRace.ID;
            rcp.Latitude = lat;
            rcp.Longitude = lng;
            //rcp.CreateRace = createRace;
            rcp.CreateRaceID = createRace.ID;

            var raceDataPoints = db.RaceCoursePoints.Where(x => x.CreateRace.ID == id);

            raceDataPoints.ToList();

            //db.RaceCoursePoints.Where(s => s.CreateRace.Any(c => c.ID == createRace.ID));

            rcp.PointNumber = raceDataPoints.Count();

            //query full database for any matching entries, add next point
            //find a way to keep displaying points and route after a point is added
            //implement old GoogleMaps API functionality for route


            //how to add point number to rcp entry

            //query to find all entries with matching createRace.ID's add one
            db.RaceCoursePoints.Add(rcp);
            db.SaveChanges();
            /*return RedirectToAction("Details", createRace);*/ //cant return here, must return to same view with previous Race point saved

        }

        
        public void GetRaceCoursePoints(int? id)
        {
            //int id = race.ID;
            CreateRace createRace = db.CreateRaces.Find(id);
            RaceCoursePoint rcp = new RaceCoursePoint();

            var raceDataPoints = db.RaceCoursePoints.Where(x => x.CreateRace.ID == id).OrderBy(a => a.PointNumber).ToList();
            //createRace.OriginLat = raceDataPoints[0].Latitude;
            //createRace.OriginLng = raceDataPoints[0].Longitude;
            //int lastPoint = raceDataPoints.Count - 1;
            //createRace.DestinationLat = raceDataPoints[lastPoint].Latitude;
            //createRace.DestinationLng = raceDataPoints[lastPoint].Longitude;

            createRace.RaceCoursePoints = raceDataPoints; ////where race point data will be stored in a list


            //foreach (var element in createRace.RaceCoursePoints)
            //{
            //    int count =+ 1;

            //    System.Console.WriteLine("Element #{0}: {1}", count, element);
            //}


            db.SaveChanges();
            //add any number of way points, seperated by commas
            //figure out logic for determining way points
            //and how to get that data out to the view
            //back to runnerDetailsView


        }

        //protected void btnGetCoordinates_Click(object sender, EventArgs e, int? id)
        //{
        //    //db instance
        //    //db find ID
        //    CreateRace createRace = db.CreateRaces.Find(id);

        //    var stringAddress = createRace.Address.ToString();
        //    GeoCode(stringAddress, id);
        //    //lblLattitude.Text = adrs.Latitude;
        //    //lblLongtitude.Text = adrs.Longitude;
        //}

        //public void GeoCode(string address, int? id)
        //{
        //    CreateRace createRace = db.CreateRaces.Find(id);
        //    //to Read the Stream
        //    StreamReader sr = null;

        //    //The Google Maps API Either return JSON or XML. We are using XML Here
        //    //Saving the url of the Google API 
        //    string url = String.Format("http://maps.googleapis.com/maps/api/geocode/xml?address=" +
        //    createRace.Address + createRace.City + createRace.State + createRace.ZipCode + "&sensor=false");

        //    //to Send the request to Web Client 
        //    WebClient wc = new WebClient();
        //    try
        //    {
        //        sr = new StreamReader(wc.OpenRead(url));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("The Error Occured" + ex.Message);
        //    }

        //    try
        //    {
        //        XmlTextReader xmlReader = new XmlTextReader(sr);
        //        bool latread = false;
        //        bool longread = false;

        //        while (xmlReader.Read())
        //        {
        //            xmlReader.MoveToElement();
        //            switch (xmlReader.Name)
        //            {
        //                case "lat":

        //                    if (!latread)
        //                    {
        //                        xmlReader.Read();
        //                        createRace.lat = xmlReader.Value.ToString();
        //                        latread = true;

        //                    }
        //                    break;
        //                case "lng":
        //                    if (!longread)
        //                    {
        //                        xmlReader.Read();
        //                        createRace.lng = xmlReader.Value.ToString();
        //                        longread = true;
        //                    }

        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An Error Occured" + ex.Message);
        //    }
        //}
    }


}

