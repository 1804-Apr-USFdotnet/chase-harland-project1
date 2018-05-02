using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Data;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Configuration;

namespace RestaurantReviews.Lib
{
    public enum SortBy { Alphabetical, Score, NumReviews };


    public class Library
    {
        private List<Model.Restaurant> restaurants;
        private IDataManager dm;

        public Library(string mode)
        {
            Init(mode);
        }

        public Library()
        {
            string mode = ConfigurationManager.AppSettings["DataManager"];
            Init(mode);

        }

        private void Init(string mode)
        {

            if (mode.ToLower() == "test")
            {
                dm = new LocalTestData();
            }
            else if (mode.ToLower() == "json")
            {
                dm = new LocalJson();
            }
            else
            {
                dm = new AwsTsqlAccessor();
            }

            restaurants = new List<Model.Restaurant>(dm.GetRestaurants());
        }

        
        public Model.Restaurant[] Sort(SortBy sortTerm, bool asc, int n)
        {
            if (n < 0)
            {
                n = restaurants.Count();
            }
            Model.Restaurant[] output = new Model.Restaurant[n];
            if (sortTerm == SortBy.Alphabetical)
            {
                if (asc)
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return string.Compare(x.Name, y.Name);
                    });
                } else
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return string.Compare(y.Name, x.Name);
                    });
                }
            }
            else if (sortTerm == SortBy.NumReviews)
            {
                if (asc)
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return x.GetReviews().Count().CompareTo(y.GetReviews().Count());
                    });
                }
                else
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return y.GetReviews().Count().CompareTo(x.GetReviews().Count());
                    });
                }
            }
            // Sort by AvgScore
            else
            {
                if (asc)
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return x.AvgScore.CompareTo(y.AvgScore);
                    });
                }
                else
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return y.AvgScore.CompareTo(x.AvgScore);
                    });
                }
            }

            return restaurants.GetRange(0, n).ToArray();
        }

        public Model.Restaurant[] Sort(SortBy sortTerm, bool asc)
        {
            return Sort(sortTerm, asc, -1);
        }


        public Model.Restaurant[] Search(string[] searchTerms)
        {
            List<Model.Restaurant> results = new List<Model.Restaurant>();
            List<int> resultScores = new List<int>();

            int n;

            foreach (Model.Restaurant r in restaurants)
            {
                n = 0;

                foreach (string term in searchTerms)
                {
                    if (r.Name.ToLower().Contains(term.ToLower()))
                    {
                        n++;
                    }
                }

                if (n > 0)
                {
                    results.Add(r);
                    resultScores.Add(n);
                }
            }

            Model.Restaurant[] output = results.ToArray();

            Array.Sort(resultScores.ToArray(), output);

            return output;
        }

        public Model.Restaurant[] Search(string term)
        {
            return Search(new string[] { term });
        }


        public Model.Restaurant[] GetRestaurants()
        {
            return restaurants.ToArray();
        }

        public Model.Review[] GetReviews(string restaurantName)
        {
            return RestLookup(restaurantName).GetReviews();
        }

        public void AddReview(int restaurantId, int score)
        {
            Model.Restaurant rest = RestLookup(restaurantId);
            int newId = dm.AddReview(new Model.Review(-1, score), restaurantId);
            rest.AddReview(new Model.Review(newId, score));
        }


        public Model.Restaurant RestLookup(string name)
        {
            foreach (Model.Restaurant r in restaurants)
            {
                if (r.Name.ToLower() == name.ToLower())
                {
                    return r;
                }
            }

            return null;
        }

        private Model.Restaurant RestLookup(int id)
        {
            foreach (Model.Restaurant r in restaurants)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }

            return null;
        }
        
    }
}
