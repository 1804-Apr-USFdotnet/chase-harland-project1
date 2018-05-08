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
        public ActionResult Index(string sort = "score",
            string asc = "false", string n = "-1")
        {
            SortBy sortScheme;

            switch (sort.ToLower())
            {
                case "alpha":
                    sortScheme = SortBy.Alphabetical;
                    break;
                case "numrev":
                    sortScheme = SortBy.NumReviews;
                    break;
                case "score":
                    sortScheme = SortBy.Score;
                    break;
                case "food":
                    sortScheme = SortBy.Food;
                    break;
                default:
                    sortScheme = SortBy.Score;
                    break;
            }

            bool ascending;
            try
            {
                ascending = bool.Parse(asc.ToLower());
            }
            catch (Exception)
            {
                ascending = false;
            }

            int topN;
            try
            {
                topN = int.Parse(n);
            }
            catch (Exception)
            {
                topN = -1;
            }

            if (topN < 0)
            {
                return View(
                    ModelConverter.Convert(
                        lib.Sort(sortScheme, ascending)));
            }
            else
            {
                return View(
                    ModelConverter.Convert(
                        lib.Sort(sortScheme, ascending, topN)));
            }
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

            Model.Restaurant r;

            try
            {
                r = lib.GetRestaurant((int)id);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

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
                if (ModelState.IsValid)
                {
                    lib.AddRestaurant(ModelConverter.Convert(r));
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r;

            try
            {
                r = lib.GetRestaurant((int)id);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

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
                if (ModelState.IsValid)
                {
                    lib.EditRestaurant(ModelConverter.Convert(r));
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r;

            try
            {
                r = lib.GetRestaurant((int)id);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

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
