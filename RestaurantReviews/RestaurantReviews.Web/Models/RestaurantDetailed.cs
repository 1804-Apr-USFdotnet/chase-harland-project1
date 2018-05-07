using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Web.Models
{
    public class RestaurantDetailed
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get { return _restaurant.Id;  }
            set { _restaurant.Id = value; }
        }
        [Required]
        [MaxLength(40, ErrorMessage = "Can be no more than 40 characters long")]
        public string Name
        {
            get { return _restaurant.Name; }
            set { _restaurant.Name = value; }
        }
        [MaxLength(60, ErrorMessage = "Can be no more than 60 characters long")]
        public string Food
        {
            get { return _restaurant.Food; }
            set { _restaurant.Food = value; }
        }

        [ScaffoldColumn(false)]
        public double AvgScore { get; private set; }
        [ScaffoldColumn(false)]
        public int NumReviews
        {
            get
            {
                return _reviews.Count();
            }
        }

        private Restaurant _restaurant;
        private List<Review> _reviews;

        public RestaurantDetailed(Restaurant r, Review[] revs, double avgScore)
        {
            _restaurant = r;
            _reviews = new List<Review>(revs);
            AvgScore = avgScore;
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