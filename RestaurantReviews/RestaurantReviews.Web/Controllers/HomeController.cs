using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantReviews.Lib;

namespace RestaurantReviews.Web.Controllers
{
    public class HomeController : Controller
    {
        private Library lib;

        public HomeController(string mode)
        {
            lib = new Library(mode);
        }

        public HomeController()
        {
            lib = new Library();
        }


        public ActionResult Index()
        {
            //var top3 = lib.Sort(SortBy.Score, false, 3);

            //return View(ModelConverter.Convert(top3));

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
    }
}