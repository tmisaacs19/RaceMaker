using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using RaceMaker.Models;

namespace RaceMaker.Controllers
{
    public class SendEmailWithAttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: /SendMailer/
        //[HttpGet]
        public ActionResult Index(int? id)
        {
            return View();
        }

        /// <summary>

        /// Send Mail with Gmail

        /// </summary>

        /// <param name="objModelMail">MailModel Object, keeps all properties</param>

        /// <param name="fileUploader">Selected file data, example-filename,content,content type(file type- .txt,.png etc.),length etc.</param>

        /// <returns></returns>

        [HttpPost]

        public ActionResult Index(RaceMaker.Models.SendEmailWithAttachment objModelMail, HttpPostedFileBase fileUploader)

        {

            if (ModelState.IsValid)

            {
                string from = "raceMaker12@gmail.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, objModelMail.To))

                {

                    mail.Subject = objModelMail.Subject;

                    mail.Body = objModelMail.Body;

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "Running2win");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    ViewBag.Message = "Sent";

                    return View("Index", objModelMail);

                }

            }

            else

            {

                return View();

            }

        }

    }

}