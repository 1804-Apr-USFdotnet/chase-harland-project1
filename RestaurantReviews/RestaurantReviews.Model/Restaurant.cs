using System.Linq;

namespace RestaurantReviews.Model
{
    public class Restaurant
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Food { get; private set; }

        private double _avgScore;

        //Lazy calculation
        public double AvgScore
        {
            get
            {
                if (_avgScore < 0)
                {
                    if (Reviews != null)
                    {
                        return _avgScore = 1.0 * Reviews.Sum(x => x.Score) / Reviews.Count();
                    }
                    else
                    {
                        return _avgScore = 0;
                    } 
                }
                else
                {
                    return _avgScore;
                }
            }
        }
        
        public Review[] Reviews { get; private set; }

        public Restaurant(int id, string name, string food, Review[] reviews)
        {
            Id = id;
            Name = name;
            Food = food;
            Reviews = reviews;
            _avgScore = -1;
        }
    }
}
