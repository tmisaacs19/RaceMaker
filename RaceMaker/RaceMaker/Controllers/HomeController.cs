using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceMaker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public static ActionResult GetUrlFromChart(this HtmlHelper helper, Chart chart)
        //{
        //    lock (object)
        //    {
        //        string path = HttpContext.Current.Server.MapPath("~/App_Data/graphs/");
        //        string filename = path + Guid.NewGuid() + ".jpg";
        //        var image = chart.ToWebImage("jpg");
        //        //save your image.
        //        return File(image.GetBytes(), "image/jpeg");
        //    }
        //}
    }
}