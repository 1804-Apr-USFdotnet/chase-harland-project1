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
    public class Review
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public int Score { get; private set; }

        public Review(int id, int score)
        {
            Score = score;
        }
    }
}
