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

        private Model.Restaurant[] restaurants;
        private Model.Review[] reviews;

        public LocalTestData()
        {
            restaurants = new Model.Restaurant[4];
            reviews = new Model.Review[13];

            restaurants[0] = new Model.Restaurant(1, "Olive_Garden");
            restaurants[1] = new Model.Restaurant(2, "Hard_Rock_Cafe");
            restaurants[2] = new Model.Restaurant(3, "Subway");
            restaurants[3] = new Model.Restaurant(4, "McDonald's");

            reviews[0] = new Model.Review(1, 3);
            reviews[1] = new Model.Review(2, 4);
            reviews[2] = new Model.Review(3, 5);
            restaurants[0].AddReview(reviews[0]);
            restaurants[0].AddReview(reviews[1]);
            restaurants[0].AddReview(reviews[2]);

            reviews[3] = new Model.Review(4, 4);
            reviews[4] = new Model.Review(5, 4);
            reviews[5] = new Model.Review(6, 5);
            restaurants[1].AddReview(reviews[3]);
            restaurants[1].AddReview(reviews[4]);
            restaurants[1].AddReview(reviews[5]);

            reviews[6] = new Model.Review(7, 4);
            reviews[7] = new Model.Review(8, 4);
            reviews[8] = new Model.Review(9, 3);
            restaurants[2].AddReview(reviews[6]);
            restaurants[2].AddReview(reviews[7]);
            restaurants[2].AddReview(reviews[8]);

            reviews[9] = new Model.Review(10, 5);
            reviews[10] = new Model.Review(11, 2);
            reviews[11] = new Model.Review(12, 1);
            reviews[12] = new Model.Review(13, 2);
            restaurants[3].AddReview(reviews[9]);
            restaurants[3].AddReview(reviews[10]);
            restaurants[3].AddReview(reviews[11]);
            restaurants[3].AddReview(reviews[12]);
        }

        public int AddRestaurant(Model.Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public int AddReview(Model.Review review, int restId)
        {
            throw new NotImplementedException();
        }

        public Model.Restaurant GetRestaurant(int id)
        {
            try
            {
                return restaurants[id - 1];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Model.Restaurant[] GetRestaurants()
        {
            return restaurants;
        }

        public Model.Review GetReview(int id)
        {
            try
            {
                return reviews[id - 1];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Model.Review[] GetReviews()
        {
            return reviews;
        }

        public bool RemoveRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveReview(int id)
        {
            throw new NotImplementedException();
        }

        public Model.Restaurant UpdateRestaurant(int id, string restname)
        {
            throw new NotImplementedException();
        }

        public Model.Review UpdateReview(int id, int revscore, int revsubject)
        {
            throw new NotImplementedException();
        }
    }
}
