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
        int AddRestaurant(Model.Restaurant r);
        bool UpdateRestaurant(Model.Restaurant r);
        bool RemoveRestaurant(int id);

        // Reviews
        Model.Review[] GetReviews();
        Model.Review[] GetReviews(int restId);
        Model.Review GetReview(int id);
        int AddReview(Model.Review rev);
        bool UpdateReview(Model.Review rev);
        bool RemoveReview(int id);
    }
}
