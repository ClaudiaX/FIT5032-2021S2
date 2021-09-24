using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_2021S2.Models
{
    public class BookEvent
    {
        [Required]
        public int StoreEventId { get; set; }
        public StoreEvent StoreEvent { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}