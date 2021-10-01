using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FIT5032_2021S2.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$",ErrorMessage ="Not a valid phone number")]
        public string ContactNumber { get; set; }

        [NotMapped]
        [Display(Name = "Rating")]
        public decimal AvgRating { get; set; } = 0;

        public List<Rating> Ratings { get; set; }
    }
}