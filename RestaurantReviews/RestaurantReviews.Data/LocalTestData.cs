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
            restaurants = new List<Model.Restaurant>();
            reviews = new List<Model.Review>();


            reviews.Add(new Model.Review(1, 3, "name1", "comment1", 1));
            reviews.Add(new Model.Review(2, 4, "name2", "comment2", 1));
            reviews.Add(new Model.Review(3, 5, "name3", "comment3", 1));
            Model.Review[] revs1 = new Model.Review[]
            { reviews[0], reviews[1], reviews[2] };

            reviews.Add(new Model.Review(4, 4, null, "comment4", 2));
            reviews.Add(new Model.Review(5, 4, "name5", "comment5", 2));
            reviews.Add(new Model.Review(6, 5, "name6", "comment6", 2));
            Model.Review[] revs2 = new Model.Review[]
            { reviews[3], reviews[4], reviews[5] };

            reviews.Add(new Model.Review(7, 4, "name7", "comment7", 3));
            reviews.Add(new Model.Review(8, 4, "name8", "comment8", 3));
            reviews.Add(new Model.Review(9, 3, null, "comment9", 3));
            Model.Review[] revs3 = new Model.Review[]
            { reviews[6], reviews[7], reviews[8] };

            reviews.Add(new Model.Review(10, 5, "name10", "comment10", 4));
            reviews.Add(new Model.Review(11, 2, "name11", null, 4));
            reviews.Add(new Model.Review(12, 1, "name12", "comment12", 4));
            reviews.Add(new Model.Review(13, 2, "name13", "comment13", 4));
            Model.Review[] revs4 = new Model.Review[]
            { reviews[9], reviews[10], reviews[11], reviews[12] };

            restaurants.Add(new Model.Restaurant(1, "Olive_Garden", "Italian", revs1));
            restaurants.Add(new Model.Restaurant(2, "Hard_Rock_Cafe", "American", revs2));
            restaurants.Add(new Model.Restaurant(3, "Subway", "Subs", revs3));
            restaurants.Add(new Model.Restaurant(4, "McDonald's", "Fast food", revs4));
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
            return restaurants.Find(x => x.Id == id);
        }

        public Model.Restaurant[] GetRestaurants()
        {
            return restaurants.ToArray();
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
            return restaurants.Remove(GetRestaurant(id));
        }

        public bool RemoveReview(int id)
        {
            return reviews.Remove(GetReview(id));
        }

        public bool UpdateRestaurant(Model.Restaurant r)
        {
            bool result = RemoveRestaurant(r.Id);
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
