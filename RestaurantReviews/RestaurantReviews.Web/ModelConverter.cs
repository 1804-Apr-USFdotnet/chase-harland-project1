using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantReviews.Model;

namespace RestaurantReviews.Web
{
    public static class ModelConverter
    {
        public static Models.Restaurant ConvertLite(Restaurant r)
        {
            return new Models.Restaurant(r.Id, r.Name, r.Food);
        }
        public static Models.Restaurant[] ConvertLite(Restaurant[] rs)
        {
            Models.Restaurant[] output = new Models.Restaurant[rs.Length];
            for (int i = 0; i < rs.Length; i++)
            {
                output[i] = ConvertLite(rs[i]);
            }

            return output;
        }

        public static Models.RestaurantDetailed Convert(Restaurant r)
        {
            Models.Restaurant rModel = ConvertLite(r);
            Models.Review[] reviews = Convert(r.Reviews, rModel);
            
            return new Models.RestaurantDetailed(rModel, reviews, r.AvgScore);
        }
        public static Models.RestaurantDetailed[] Convert(Restaurant[] rs)
        {
            Models.RestaurantDetailed[] output = new Models.RestaurantDetailed[rs.Length];
            for (int i = 0; i < rs.Length; i++)
            {
                output[i] = Convert(rs[i]);
            }

            return output;
        }

        public static Models.Review Convert(Review rev, Models.Restaurant r)
        {
            return new Models.Review(rev.Id, rev.Score, rev.Reviewer, rev.Comment, r);
        }
        public static Models.Review[] Convert(Review[] revs, Models.Restaurant r)
        {
            Models.Review[] output = new Models.Review[revs.Length];
            for(int i = 0; i < revs.Length; i++)
            {
                output[i] = Convert(revs[i], r);
            }

            return output;
        }

        public static Restaurant Convert(Models.Restaurant r)
        {
            return new Restaurant(r.Id, r.Name, r.Food, null);
        }
        public static Review Convert(Models.Review rev)
        {
            return new Review(rev.Id, rev.Score, rev.Reviewer, rev.Comment, rev.Subject.Id);
        }
    }
}