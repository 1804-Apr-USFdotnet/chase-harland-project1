using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Model;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.IO;

namespace RestaurantReviews.Data
{
    public class LocalJson : IDataManager
    {
        private string path;

        public LocalJson()
        {
            path = ConfigurationManager.AppSettings["SerializePath"];
        }

        public int AddRestaurant(Model.Restaurant restaurant)
        {
            var list = ReadJson();
            list.Add(restaurant);
            WriteJson(list);
            return list.Count();
        }

        public int AddReview(Model.Review review, int restId)
        {
            var list = ReadJson();
            foreach (var r in list)
            {
                if (r.Id == restId)
                {
                    r.AddReview(review);
                    break;
                }
            }
            WriteJson(list);
            return review.Id;
        }

        public Model.Restaurant GetRestaurant(int id)
        {
            var list = ReadJson();
            foreach (var r in list)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }

            return null;
        }

        public Model.Restaurant[] GetRestaurants()
        {
            return ReadJson().ToArray();
        }

        public Model.Review GetReview(int id)
        {
            var list = ReadJson();
            foreach(var r in list)
            {
                foreach(var rev in r.GetReviews())
                {
                    if (rev.Id == id)
                    {
                        return rev;
                    }
                }
            }

            return null;
        }

        public Model.Review[] GetReviews()
        {
            var list = ReadJson();
            List<Model.Review> output = new List<Model.Review>();

            foreach (var r in list)
            {
                output.AddRange(r.GetReviews());
            }

            return output.ToArray();
        }

        public bool RemoveRestaurant(int id)
        {
            var list = ReadJson();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).Id == id)
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveReview(int id)
        {
            var list = ReadJson();

            foreach (var r in list)
            {
                foreach (var rev in r.GetReviews())
                {
                    if (rev.Id == id)
                    {
                        return r.RemoveReview(rev);
                    }
                }
            }

            return false;
        }

        public Model.Restaurant UpdateRestaurant(int id, string restname)
        {
            throw new NotImplementedException();
        }

        public Model.Review UpdateReview(int id, int revscore, int revsubject)
        {
            throw new NotImplementedException();
        }

        private List<Model.Restaurant> ReadJson()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = File.Open(path,
                    FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Model.Restaurant>));
                    fs.CopyTo(ms);
                    return ((List<Model.Restaurant>)ser.ReadObject(ms));
                }
            }
        }

        private void WriteJson(List<Model.Restaurant> restaurants)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = File.Open(path,
                    FileMode.Open, FileAccess.ReadWrite))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Model.Restaurant>));
                    ser.WriteObject(ms, restaurants);
                    ms.Position = 0;
                    ms.CopyTo(fs);
                }
            }
        }
    }
}
