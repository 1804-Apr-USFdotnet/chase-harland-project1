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
            Restaurant r = new Restaurant(1, "name", "good", new Review[]
            {
                new Review(1, 2, "", "", 1),
                new Review(2, 3, "", "", 1)
            });
            
            
            Assert.AreEqual(r.AvgScore, 2.5);
        }
    }
}
