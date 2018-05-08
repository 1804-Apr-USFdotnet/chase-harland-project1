using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using RestaurantReviews.Lib;

namespace RestaurantReviews.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private Library lib;

        public RestaurantController(string mode)
        {
            lib = new Library(mode);
        }

        public RestaurantController()
        {
            lib = new Library();
        }


        // GET: Restaurant
        public ActionResult Index()
        {
            return View(
                ModelConverter.Convert(
                    lib.GetRestaurants()));
        }

        public ActionResult Search(string query)
        {
            Model.Restaurant[] results = lib.SearchAndParse(query);

            return View("Index",
                ModelConverter.Convert(results));
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r = lib.GetRestaurant((int)id);

            if (r == null)
            {
                return HttpNotFound();
            }

            TempData["restId"] = id;

            return View(ModelConverter.Convert(r));
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Food")] Models.Restaurant r)
        {
            try
            {
                lib.AddRestaurant(ModelConverter.Convert(r));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r = lib.GetRestaurant((int)id);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(ModelConverter.ConvertLite(r));
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Restaurant r)
        {
            try
            {
                lib.EditRestaurant(ModelConverter.Convert(r));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var r = lib.GetRestaurant((int)id);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(ModelConverter.Convert(r));
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                lib.RemoveRestaurant((int)id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
