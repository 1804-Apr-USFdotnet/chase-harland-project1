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
                    restname = restaurant.Name
                });

                context.SaveChanges();
                log.Info("Added restaurant: " + newRest.restname);
                return newRest.restid;
            }
        }

        public int AddReview(Model.Review review, int restId)
        {
            using (var context = new RestaurantDBEntities())
            {
                Review newRev = context.Reviews.Add(new Review()
                {
                    revscore = review.Score,
                    revsubject = restId
                });

                context.SaveChanges();
                log.Info("Added review: " + review.Id);
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
                var rests = context.Restaurants.Count();
                Model.Restaurant[] output = new Model.Restaurant[context.Restaurants.Count()];
                int i = 0;
                foreach (var r in context.Restaurants)
                {
                    output[i++] = Convert(r);
                }

                return output;
            }
        }

        public Model.Review GetReview(int id)
        {
            using (var context = new RestaurantDBEntities())
            {
                return Convert(context.Reviews.Find(id));
            }
        }

        public Model.Review[] GetReviews()
        {
            using (var context = new RestaurantDBEntities())
            {
                Model.Review[] output = new Model.Review[context.Reviews.Count()];
                int i = 0;
                foreach (var r in context.Reviews)
                {
                    output[i++] = Convert(r);
                }

                return output;
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

        public Model.Restaurant UpdateRestaurant(int id, string restname)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Restaurants.Find(id);
                std.restname = restname;

                context.SaveChanges();
                log.Info("Updated restaurant " + id);
                return Convert(std);
            }
        }

        public Model.Review UpdateReview(int id, int revscore, int revsubject)
        {
            using (var context = new RestaurantDBEntities())
            {
                var std = context.Reviews.Find(id);
                std.revscore = revscore;
                std.revsubject = revsubject;

                context.SaveChanges();
                log.Info("Updated review " + id);
                return Convert(std);
            }
        }


        private Model.Restaurant Convert(Restaurant rest)
        {
            Model.Restaurant output = new Model.Restaurant(rest.restid, rest.restname);
            foreach (Review rev in rest.Reviews)
            {
                output.AddReview(Convert(rev));
            }
            return output;
        }

        private Model.Review Convert(Review rev)
        {
            return new Model.Review(rev.revid, rev.revscore);
        }
    }
}
