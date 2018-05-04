using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Model;

namespace RestaurantReviews.Data
{
    public class LocalTestData : IDataManager
    {

        private List<Model.Restaurant> restaurants;
        private List<Model.Review> reviews;

        public LocalTestData()
        {
            restaurants = new List<Model.Restaurant>
            {
                new Model.Restaurant(1, "Olive Garden", "Italian", null),
                new Model.Restaurant(2, "Hard Rock Cafe", "American", null),
                new Model.Restaurant(3, "Subway", "Subs", null),
                new Model.Restaurant(4, "McDonald's", "Fast food", null)
            };
        
            reviews = new List<Model.Review>
            {
                new Model.Review(1, 3, "name1", "comment1", 1),
                new Model.Review(2, 4, "name2", "comment2", 1),
                new Model.Review(3, 5, "name3", "comment3", 1),

                new Model.Review(4, 4, null, "comment4", 2),
                new Model.Review(5, 4, "name5", "comment5", 2),
                new Model.Review(6, 5, "name6", "comment6", 2),

                new Model.Review(7, 4, "name7", "comment7", 3),
                new Model.Review(8, 4, "name8", "comment8", 3),
                new Model.Review(9, 3, null, "comment9", 3),

                new Model.Review(10, 5, "name10", "comment10", 4),
                new Model.Review(11, 2, "name11", null, 4),
                new Model.Review(12, 1, "name12", "comment12", 4),
                new Model.Review(13, 2, "name13", "comment13", 4)
            };
        }

        public int AddRestaurant(Model.Restaurant r)
        {
            int[] ids = restaurants.Select(x => x.Id).ToArray();
            int i = r.Id;
            while (ids.Contains(i))
            {
                i++;
            }

            restaurants.Add(new Model.Restaurant(i, r.Name, r.Food, r.Reviews));
            return i;
        }

        public int AddReview(Model.Review rev)
        {
            int[] ids = reviews.Select(x => x.Id).ToArray();
            int i = rev.Id;
            while (ids.Contains(i))
            {
                i++;
            }

            reviews.Add(new Model.Review(i, rev.Score, rev.Reviewer, rev.Comment, rev.Subject));
            return i;
        }
        
        public Model.Restaurant GetRestaurant(int id)
        {
            var r = restaurants.Find(x => x.Id == id);
            if (r == null)
            {
                return null;
            }
            return new Model.Restaurant(
                r.Id, r.Name, r.Food,
                reviews.Where(x => x.Subject == id).ToArray());
        }

        public Model.Restaurant[] GetRestaurants()
        {
            Model.Restaurant[] output = new Model.Restaurant[restaurants.Count()];
            Model.Restaurant r;
            for(int i = 0; i < output.Length; i++)
            {
                r = restaurants.ElementAt(i);
                output[i] = new Model.Restaurant(
                    r.Id, r.Name, r.Food,
                    reviews.Where(x => x.Subject == r.Id).ToArray());
            }
            return output;
        }

        public Model.Review GetReview(int id)
        {
            return reviews.Find(x => x.Id == id);
        }

        public Model.Review[] GetReviews(int restId)
        {
            return reviews.Where(x => x.Subject == restId).ToArray();
        }

        public Model.Review[] GetReviews()
        {
            return reviews.ToArray();
        }

        public bool RemoveRestaurant(int id)
        {
            return RemoveRestaurant(id, true);
        }

        private bool RemoveRestaurant(int id, bool cascade)
        {
            bool output = restaurants.Remove(GetRestaurant(id));
            if (cascade)
            {
                reviews.RemoveAll(x => x.Subject == id);
            }

            return output;
        }

        public bool RemoveReview(int id)
        {
            return reviews.Remove(GetReview(id));
        }

        public bool UpdateRestaurant(Model.Restaurant r)
        {
            bool result = RemoveRestaurant(r.Id, false);
            if (result)
            {
                AddRestaurant(r);
            }
            return result;
        }

        public bool UpdateReview(Model.Review rev)
        {
            bool result = RemoveReview(rev.Id);
            if (result)
            {
                AddReview(rev);
            }
            return result;
        }
    }
}
