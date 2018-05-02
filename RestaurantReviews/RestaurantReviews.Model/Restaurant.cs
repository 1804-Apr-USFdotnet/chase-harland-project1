using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RestaurantReviews.Model
{
    [DataContract]
    public class Restaurant
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public double AvgScore { get; private set; }
        [DataMember]
        private List<Review> reviews;

        public Restaurant(int id, string name)
        {
            Id = id;
            Name = name;
            AvgScore = 0;
            reviews = new List<Review>();
        }

        public void AddReview(Review r)
        {
            reviews.Add(r);
            AvgScore = AvgScore * (reviews.Count - 1) / reviews.Count + r.Score / ((double)reviews.Count);
        }

        public void AddReviews(Review[] revs)
        {
            foreach (Review r in revs)
            {
                AddReview(r);
            }
        }

        public bool RemoveReview(Review r)
        {
            return reviews.Remove(r);
        }

        public Review[] GetReviews()
        {
            return reviews.ToArray();
        }
    }
}
