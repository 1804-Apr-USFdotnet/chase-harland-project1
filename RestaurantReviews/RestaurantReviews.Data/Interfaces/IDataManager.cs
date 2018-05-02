using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Model;
using System.Data.Entity;

namespace RestaurantReviews.Data
{
    public interface IDataManager
    {
        // CRUD methods
        // Restaurants
        Model.Restaurant[] GetRestaurants();
        Model.Restaurant GetRestaurant(int id);
        int AddRestaurant(Model.Restaurant restaurant);
        Model.Restaurant UpdateRestaurant(int id, string restname);
        bool RemoveRestaurant(int id);

        // Reviews
        Model.Review[] GetReviews();
        Model.Review GetReview(int id);
        int AddReview(Model.Review review, int restId);
        Model.Review UpdateReview(int id, int revscore, int revsubject);
        bool RemoveReview(int id);
    }
}
