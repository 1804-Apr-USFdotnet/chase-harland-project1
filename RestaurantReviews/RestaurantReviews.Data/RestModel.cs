using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    [Table("rest")]
    class RestModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Restaurant Name should be within 25 characters")]
        public string Naee { get; set; }
        [Column("s1")]
        public string Street1 { get; set; }
        [Column("s2")]
        public string Street2 { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [NotMapped]
        public int? AvgRating { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
