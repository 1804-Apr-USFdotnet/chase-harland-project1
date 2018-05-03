using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Model
{
    [Table("rev")]
    class Review2
    {
        [Column("Id")]
        public int ReviewsId { get; set; }
        [Required]
        [Range(1,10, ErrorMessage = "Rating should be between 1 and 10")]
        public int Rating { get; set; }
        [StringLength(200, ErrorMessage ="Comment should not be longer than 200 characters")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        
        public virtual Restaurant2 Restaurant { get; set; }
    }
}
