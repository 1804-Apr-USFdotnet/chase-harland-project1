using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Data;

namespace RestaurantReviews.DataTest
{
    [TestClass]
    public class LocalTestDataTest
    {
        [TestMethod]
        public void LTDGetResTest()
        {
            IDataManager dm = new LocalTestData();

            var r = dm.GetRestaurant(1);

            Assert.AreEqual(r.Id, 1);
            Assert.IsNotNull(r.Reviews);
        }

        [TestMethod]
        public void LTDGetAllResTest()
        {
            IDataManager dm = new LocalTestData();

            var allR = dm.GetRestaurants();

            Assert.AreEqual(allR.Length, 4);
            Assert.IsNotNull(allR[0].Reviews);
        }

        [TestMethod]
        public void LTDGetRevTest()
        {
            IDataManager dm = new LocalTestData();

            var rev = dm.GetReview(1);

            Assert.AreEqual(rev.Id, 1);
        }

        [TestMethod]
        public void LTDGetAllRevTest()
        {
            IDataManager dm = new LocalTestData();

            var revs = dm.GetReviews();

            Assert.AreEqual(revs.Length, 13);
            Assert.AreEqual(revs[0].Id, 1);
            Assert.AreEqual(revs[12].Id, 13);
        }

        [TestMethod]
        public void LTDGetResRevTest()
        {
            IDataManager dm = new LocalTestData();

            var revs = dm.GetReviews(1);

            Assert.AreEqual(revs.Length, 3);
            Assert.AreEqual(revs[0].Subject, 1);
            Assert.AreEqual(revs[1].Subject, 1);
            Assert.AreEqual(revs[2].Subject, 1);
        }

        [TestMethod]
        public void LTDAddResTest()
        {
            IDataManager dm = new LocalTestData();

            var r = new Model.Restaurant(0, "newName", "newFood", null);
            int id = dm.AddRestaurant(r);

            var getR = dm.GetRestaurant(id);

            Assert.AreEqual(r.Name, getR.Name);
            Assert.AreEqual(r.Food, getR.Food);

        }

        [TestMethod]
        public void LTDAddRevTest()
        {
            IDataManager dm = new LocalTestData();

            var rev = new Model.Review(0, 3, "newRev", "newCom", 2);
            int id = dm.AddReview(rev);

            var getR = dm.GetReview(id);

            Assert.AreEqual(rev.Score, getR.Score);
            Assert.AreEqual(rev.Reviewer, getR.Reviewer);
            Assert.AreEqual(rev.Comment, getR.Comment);
        }

        [TestMethod]
        public void LTDDelResTest()
        {
            IDataManager dm = new LocalTestData();

            bool result = dm.RemoveRestaurant(1);
            var nullRef = dm.GetRestaurant(1);

            Assert.IsTrue(result);
            Assert.IsNull(nullRef);
        }

        [TestMethod]
        public void LTDDelResCascTest()
        {
            IDataManager dm = new LocalTestData();

            dm.RemoveRestaurant(1);
            var nullRef1 = dm.GetReview(1);
            var nullRef2 = dm.GetReview(2);
            var nullRef3 = dm.GetReview(3);

            Assert.IsNull(nullRef1);
            Assert.IsNull(nullRef2);
            Assert.IsNull(nullRef3);
        }

        [TestMethod]
        public void LTDDelRevTest()
        {
            IDataManager dm = new LocalTestData();

            bool result = dm.RemoveReview(1);
            var nullRef = dm.GetReview(1);

            Assert.IsTrue(result);
            Assert.IsNull(nullRef);
        }

        [TestMethod]
        public void LTDUpdateResTest()
        {
            IDataManager dm = new LocalTestData();

            var r = dm.GetRestaurant(2);
            bool result = dm.UpdateRestaurant(new Model.Restaurant(r.Id, "Stupid Cafe", r.Food, null));

            r = dm.GetRestaurant(2);

            var notNull = dm.GetReview(4);

            Assert.IsTrue(result);
            Assert.AreEqual(r.Name, "Stupid Cafe");
            Assert.IsNotNull(notNull);
        }

        [TestMethod]
        public void LTDUpdateRevTest()
        {
            IDataManager dm = new LocalTestData();

            var rev = dm.GetReview(2);
            bool result = dm.UpdateReview(new Model.Review(rev.Id, rev.Score, rev.Reviewer, "newComment", rev.Subject));

            rev = dm.GetReview(2);

            Assert.IsTrue(result);
            Assert.AreEqual(rev.Comment, "newComment");
        }
    }
}
