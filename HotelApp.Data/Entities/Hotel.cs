using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string City { get; set; }
        public IList<Room> Rooms { get; set; }

    }
}
