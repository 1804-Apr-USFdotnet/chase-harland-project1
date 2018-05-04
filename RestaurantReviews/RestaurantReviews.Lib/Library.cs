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
            else
            {
                dm = new AwsTsqlAccessor();
            }
        }

        
        public Model.Restaurant[] Sort(SortBy sortTerm, bool asc, int n)
        {
            List<Model.Restaurant> restaurants = new List<Model.Restaurant>();

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
                        return x.Reviews.Count().CompareTo(y.Reviews.Count());
                    });
                }
                else
                {
                    restaurants.Sort(delegate (Model.Restaurant x, Model.Restaurant y)
                    {
                        return y.Reviews.Count().CompareTo(x.Reviews.Count());
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
            List<Model.Restaurant> restaurants = new List<Model.Restaurant>(dm.GetRestaurants());
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
            return dm.GetRestaurants();
        }

        public Model.Restaurant GetRestaurant(int id)
        {
            return dm.GetRestaurant(id);
        }

        public Model.Review[] GetReviews(int id)
        {
            return dm.GetRestaurant(id).Reviews;
        }

        public void AddReview(Model.Review rev)
        {
            dm.AddReview(rev);
        }

        public bool EditReview(Model.Review rev)
        {
            return dm.UpdateReview(rev);
        }
        
    }
}
