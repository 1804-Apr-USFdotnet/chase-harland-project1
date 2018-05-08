using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Model;
using System.Data.Entity;
using NLog;

namespace RestaurantReviews.Data
{
    public class AwsTsqlAccessor : IDataManager
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public int AddRestaurant(Model.Restaurant restaurant)
        {
            using (var context = new RestaurantDBEntities())
            {
                Restaurant newRest = context.Restaurants.Add(new Restaurant()
                {
                    restname = restaurant.Name,
                    food = restaurant.Food
                });

                context.SaveChanges();
                log.Info("Added restaurant: " + newRest.restid + ": " + newRest.restname);
                return newRest.restid;
            }
        }

        public int AddReview(Model.Review review)
        {
            using (var context = new RestaurantDBEntities())
            {
                Review newRev = context.Reviews.Add(new Review()
                {
                    revscore = review.Score,
                    reviewer = review.Reviewer,
                    comment = review.Comment,
                    revsubject = review.Subject
                });

                context.SaveChanges();
                log.Info("Added review: " + newRev.revid);
                return newRev.revid;
            }
        }

        public Model.Restaurant GetRestaurant(int id)
        {
            using (var context = new RestaurantDBEntities())
            {
                return Convert(context.Restaurants.Find(id));
            }
        }

        public Model.Restaurant[] GetRestaurants()
        {
            using (var context = new RestaurantDBEntities())
            {
                return Convert(context.Restaurants.ToArray());
            }
        }

        public Model.Review GetReview(int id)
        {
            using (var context = new RestaurantDBEntities())
            {
                return Convert(context.Reviews.Find(id));
            }
        }

        public Model.Review[] GetReviews(int restId)
        {
            using (var context = new RestaurantDBEntities())
            {
                return context.Reviews
                    .Where(x => x.revsubject == restId)
                    .Select(x => Convert(x))
                    .ToArray();
            }
        }

        public Model.Review[] GetReviews()
        {
            using (var context = new RestaurantDBEntities())
            {
                return context.Reviews
                    .Select(x => Convert(x))
                    .ToArray();
            }
        }

        public bool RemoveRestaurant(int id)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Restaurants.Find(id);
                context.Restaurants.Remove(std);
                context.SaveChanges();
                if (std != null)
                {
                    log.Info("Removed restaurant " + id);
                }
                else
                {
                    log.Info("Failed to remove restaurant " + id);
                }
                
                return std != null;
            }
        }

        public bool RemoveReview(int id)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Reviews.Find(id);
                context.Reviews.Remove(std);

                context.SaveChanges();
                if (std != null)
                {
                    log.Info("Removed review " + id);
                }
                else
                {
                    log.Info("Failed to remove review " + id);
                }
                return std != null;
            }
        }

        public bool UpdateRestaurant(Model.Restaurant r)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Restaurants.Find(r.Id);
                if (std == null)
                {
                    log.Error("Update failed to locate restaurant: " + r.Id);
                    return false;
                }
                std.restname = r.Name;
                std.food = r.Food;

                context.SaveChanges();
                log.Info("Updated restaurant " + r.Id);
                return true;
            }
        }

        public bool UpdateReview(Model.Review rev)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Reviews.Find(rev.Id);
                if (std == null)
                {
                    log.Error("Update failed to locate Review: " + rev.Id);
                    return false;
                }
                std.revscore = rev.Score;
                std.reviewer = rev.Reviewer;
                std.comment = rev.Comment;
                std.revsubject = rev.Subject;

                context.SaveChanges();
                log.Info("Updated review " + rev.Id);
                return true;
            }
        }


        private Model.Restaurant Convert(Restaurant rest)
        {
            return new Model.Restaurant(rest.restid, rest.restname,
                rest.food, rest.Reviews.Select(x => Convert(x)).ToArray());
        }

        private Model.Restaurant[] Convert(Restaurant[] rests)
        {
            return rests.Select(x => Convert(x)).ToArray();
        }

        private Model.Review Convert(Review rev)
        {
            return new Model.Review(rev.revid, rev.revscore,
                rev.reviewer, rev.comment, rev.revsubject);
        }
    }
}
