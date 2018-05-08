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

            ViewBag.restId = restId;

            Model.Restaurant r = lib.GetRestaurant((int)restId);

            if (r == null)
            {
                return HttpNotFound();
            }

            Model.Review[] revs = lib.GetReviews((int)restId);

            var revModels =
                ModelConverter.Convert(revs,
                ModelConverter.ConvertLite(r));

            return View(revModels);
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Review rev;

            try
            {
                rev = lib.GetReview((int)id);
            }
            catch (Exception)
            {
                return View();
            }

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

            TempData["restId"] = restId;

            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Score, Reviewer, Comment")] Models.Review rev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rev.SubjectID = (int)TempData["restId"];
                    lib.AddReview(ModelConverter.Convert(rev));

                    return RedirectToAction("Details", "Restaurant", new { id = rev.SubjectID }); 
                }
            }
            catch
            {
                return View();
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Review rev;

            try
            {
                rev = lib.GetReview((int)id);
            }
            catch (Exception)
            {
                return View();
            }

            if (rev == null)
            {
                return HttpNotFound();
            }

            Model.Restaurant r = lib.GetRestaurant(rev.Subject);

            if (r == null)
            {
                return HttpNotFound();
            }

            TempData["restId"] = rev.Subject;

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
                if (ModelState.IsValid)
                {
                    rev.SubjectID = (int)TempData["restId"];
                    bool result = lib.EditReview(ModelConverter.Convert(rev));

                    if (!result)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    return RedirectToAction("Details", new { id = rev.Id }); 
                }
            }
            catch
            {
                return View();
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Model.Review rev;

            try
            {
                rev = lib.GetReview((int)id);
            }
            catch (Exception)
            {
                return View();
            }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
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

                return RedirectToAction("Details", "Restaurant", new { id = rev.Subject});
            }
            catch
            {
                return View();
            }
        }
    }
}
