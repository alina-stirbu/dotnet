using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Model.Hotel
{
    using System.ComponentModel.DataAnnotations;

    public class HotelResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string City { get; set; }
    }
}
