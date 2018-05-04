using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Lib;
using RestaurantReviews.Model;

namespace RestaurantReviews.LibTest
{
    [TestClass]
    public class SortTest
    {
        [TestMethod]
        public void SortTest1()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.Alphabetical;
            bool asc = true;
            int n = 3;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc, n);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "Hard Rock Cafe");
            Assert.AreEqual(restaurants[1].Name, "McDonald's");
            Assert.AreEqual(restaurants[2].Name, "Olive Garden");
        }

        [TestMethod]
        public void SortTest2()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.Score;
            bool asc = true;
            int n = 3;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc, n);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "McDonald's");
            Assert.AreEqual(restaurants[1].Name, "Subway");
            Assert.AreEqual(restaurants[2].Name, "Olive Garden");
        }


        [TestMethod]
        public void SortTest3()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.Score;
            bool asc = false;
            int n = 3;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc, n);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "Hard Rock Cafe");
            Assert.AreEqual(restaurants[1].Name, "Olive Garden");
            Assert.AreEqual(restaurants[2].Name, "Subway");
        }

        [TestMethod]
        public void SortTest4()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.Score;
            bool asc = false;
            int n = 4;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "Hard Rock Cafe");
            Assert.AreEqual(restaurants[1].Name, "Olive Garden");
            Assert.AreEqual(restaurants[2].Name, "Subway");
            Assert.AreEqual(restaurants[3].Name, "McDonald's");
        }


        [TestMethod]
        public void SortTest5()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.NumReviews;
            bool asc = false;
            int n = 4;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "McDonald's");
        }


        [TestMethod]
        public void SortTest6()
        {
            Library lib = new Library("test");
            SortBy sortScheme = SortBy.NumReviews;
            bool asc = false;
            int n = 1;

            Restaurant[] restaurants = lib.Sort(sortScheme, asc, 1);

            Assert.AreEqual(restaurants.Length, n);
            Assert.AreEqual(restaurants[0].Name, "McDonald's");
        }
    }
}
