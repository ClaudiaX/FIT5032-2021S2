using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_2021S2.Models
{
    public class StoreEvent
    {
        public int Id { get; set; }

        [Required]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Required]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh:mm tt}")]
        public DateTime EndTime { get; set; }
    }
}