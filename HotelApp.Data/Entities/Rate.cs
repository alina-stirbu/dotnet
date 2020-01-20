using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Rate
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
        [Required]
        public DateTime Day { get; set; }

    }
}
