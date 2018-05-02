using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Model;

namespace RestaurantReviews.ModelTest
{
    [TestClass]
    public class RestaurantTest
    {
        [TestMethod]
        public void AvgScoreTest1()
        {
            Restaurant r = new Restaurant(1, "name");
            r.AddReview(new Review(1, 2));

            Assert.AreEqual(r.AvgScore, 2);

            r.AddReview(new Review(2, 3));
            Assert.AreEqual(r.AvgScore, 2.5);
        }
    }
}
