using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Web.Models
{
    public class Restaurant
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Can be no more than 40 characters long")]
        public string Name { get; set; }
        [Display(Name = "Type of food", ShortName = "Food")]
        [MaxLength(60, ErrorMessage = "Can be no more than 60 characters long")]
        public string Food { get; set; }

        public Restaurant(int id, string name, string food)
        {
            Id = id;
            Name = name;
            Food = food;
        }

        public Restaurant() { }
    }
}