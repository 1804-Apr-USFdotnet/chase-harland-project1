using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Web.Models
{
    public class RestaurantDetailed : Restaurant
    {
        [ScaffoldColumn(false)]
        public double AvgScore { get; private set; }
        
        private List<Review> _reviews;

        public RestaurantDetailed(int id, string name, string food, double avgScore, Review[] reviews) :
            base(id, name ,food)
        {
            AvgScore = avgScore;
            _reviews = new List<Review>(reviews);

        }

        public Review[] GetReviews()
        {
            return _reviews.ToArray();
        }

        public Review GetReview(int id)
        {
            return _reviews.Find(x => x.Id == id);
        }

        public void AddReview(Review review)
        {
            _reviews.Add(review);
        }

        public bool RemoveReview(int id)
        {
            return _reviews.Remove(_reviews.FirstOrDefault(x => x.Id == id));
        }
    }
}