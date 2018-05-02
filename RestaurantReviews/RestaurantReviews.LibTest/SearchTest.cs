using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Lib;
using RestaurantReviews.Model;

namespace RestaurantReviews.LibTest
{
    [TestClass]
    public class SearchTest
    {
        [TestMethod]
        public void SearchTest1()
        {
            Library lib = new Library("test");

            Restaurant[] results = lib.Search("Rock");

            Assert.AreEqual(results[0].Name, "Hard_Rock_Cafe");
        }

        [TestMethod]
        public void SearchTest2()
        {
            Library lib = new Library("test");
            Restaurant[] restaurants = lib.GetRestaurants();

            Restaurant[] results = lib.Search("_");

            Assert.IsTrue(Array.FindIndex(results, x => x.Name == "Olive_Garden") >= 0);
            Assert.IsTrue(Array.FindIndex(results, x => x.Name == "Hard_Rock_Cafe") >= 0);
        }

        [TestMethod]
        public void SearchTest3()
        {
            Library lib = new Library("test");

            Restaurant[] results = lib.Search("sub");

            Assert.AreEqual(results[0].Name, "Subway");
        }

        [TestMethod]
        public void SearchTest4()
        {
            Library lib = new Library("test");

            Restaurant[] results = lib.Search("'");

            Assert.AreEqual(results[0].Name, "McDonald's");
        }
    }
}
