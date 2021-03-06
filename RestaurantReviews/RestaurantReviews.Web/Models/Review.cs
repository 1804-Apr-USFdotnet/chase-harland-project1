﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Web.Models
{
    public class Review
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [RegularExpression("([0-5])", ErrorMessage = "Please enter valid Number")]
        [Range(0,5, ErrorMessage = "Rating must be 0-5")]
        public int Score { get; set; }
        [Required]
        [Display(Name = "Restaurant")]
        public int SubjectID {
            get { return Subject.Id; }
            set { Subject.Id = value; } }
        [MaxLength(40, ErrorMessage = "Can be no more than 40 characters long")]
        public string Reviewer { get; set; }
        [MaxLength(200, ErrorMessage = "Can be no more than 200 characters long")]
        public string Comment { get; set; }

        [Required]
        public Restaurant Subject { get; set; }

        public Review(int id, int score, string reviewer, string comment, Restaurant subject)
        {
            Id = id;
            Score = score;
            Reviewer = reviewer;
            Comment = comment;
            Subject = subject;
        }

        public Review()
        {
            Reviewer = "Anonymous";
            Comment = "N/A";
            Subject = new Restaurant();
        }
    }
}