
namespace RestaurantReviews.Model
{
    public class Review
    {
        public int Id { get; private set; }
        public int Score { get; private set; }
        public string Reviewer { get; private set; }
        public string Comment { get; private set; }
        public int Subject { get; private set; }

        public Review(int id, int score, string reviewer, string comment, int subject)
        {
            Id = id;
            Score = score;
            Reviewer = reviewer;
            Comment = comment;
            Subject = subject;
        }
    }
}
