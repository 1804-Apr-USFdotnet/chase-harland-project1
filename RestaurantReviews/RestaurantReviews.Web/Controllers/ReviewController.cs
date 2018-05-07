using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using RestaurantReviews.Lib;

namespace RestaurantReviews.Web.Controllers
{
    public class ReviewController : Controller
    {
        private Library lib;

        public ReviewController(string mode)
        {
            lib = new Library(mode);
        }

        public ReviewController()
        {
            lib = new Library();
        }

        // GET: Review
        public ActionResult Index(int? restId)
        {
            if (restId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r = lib.GetRestaurant((int)restId);

            if (r == null)
            {
                return HttpNotFound();
            }
            
            var rest = ModelConverter.Convert(r);

            return View(r);
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Review rev = lib.GetReview((int)id);

            if (rev == null)
            {
                return HttpNotFound();
            }

            Model.Restaurant r = lib.GetRestaurant(rev.Subject);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(
                ModelConverter.Convert(rev,
                ModelConverter.ConvertLite(r)));
        }

        // GET: Review/Create
        public ActionResult Create(int? restId)
        {
            if (restId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Restaurant r = lib.GetRestaurant((int)restId);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(ModelConverter.ConvertLite(r));
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Review rev)
        {
            try
            {
                lib.AddReview(ModelConverter.Convert(rev));

                return RedirectToAction("Index", new { id = rev.Subject });
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Review rev = lib.GetReview((int)id);

            if (rev == null)
            {
                return HttpNotFound();
            }

            Model.Restaurant r = lib.GetRestaurant(rev.Subject);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(
                ModelConverter.Convert(rev,
                ModelConverter.ConvertLite(r)));
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Review rev)
        {

            try
            {
                lib.EditReview(ModelConverter.Convert(rev));

                return RedirectToAction("Index", new { id = rev.Subject });
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rev = lib.GetReview((int)id);
            
            if (rev == null)
            {
                return HttpNotFound();
            }

            var r = lib.GetRestaurant(rev.Subject);

            if (r == null)
            {
                return HttpNotFound();
            }

            return View(ModelConverter.Convert(rev,
                ModelConverter.ConvertLite(r)));
        }

        // POST: Review/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Model.Review rev = lib.GetReview((int)id);
                bool result = lib.RemoveReview((int)id);

                if (!result)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index", new { id = rev.Subject });
            }
            catch
            {
                return View();
            }
        }
    }
}
