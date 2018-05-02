using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Web.Models
{
    public class Review
    {
        public int Score { get; private set; }
        public Restaurant Restaurant { get; private set; }

        public Review (int score, Restaurant restaurant)
        {
            Score = score;
            Restaurant = restaurant;
        }
    }
}