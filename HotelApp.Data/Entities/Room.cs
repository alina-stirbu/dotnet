using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Room
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string RoomName { get; set; }

        [Required]
        public int AdultsNumber { get; set; }

        public int ChildrenNumber { get; set; }

        public Hotel Hotel { get; set; }

        public IList<Rate> Rates { get; set; }
    }
}
