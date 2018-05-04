using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Web.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        
        public string Adress
        {
            get
            {
                return $"{Street1}, {Street2}, {City}, {State}, {Country}, {Zipcode}";
            }
        }

        public static IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>(new Restaurant[]
            {
                new Restaurant(){Street1="Elden st", Street2="123", Name = "Paradise1", City="Reston", State="VA", Country="US", Zipcode="10130"},
                new Restaurant(){Street1="Elden st", Street2="123", Name = "Paradise2", City="Reston", State="VA", Country="US", Zipcode="10230"},
                new Restaurant(){Street1="Elden st", Street2="123", Name = "Paradise3", City="Reston", State="VA", Country="US", Zipcode="10330"}
            });

            return restaurants;
        }

    }   
}       