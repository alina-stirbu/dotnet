using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Model.Rate
{
    using System.ComponentModel.DataAnnotations;

        public class CreateRateResource
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }
        [Required]
        public DateTime Day { get; set; }
    }
}
