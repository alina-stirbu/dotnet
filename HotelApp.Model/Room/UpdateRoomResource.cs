using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Model.Room
{
    using System.ComponentModel.DataAnnotations;

        public class UpdateRoomResource
    {
        [Required]
        [StringLength(200)]
        public string RoomName { get; set; }

        [Required]
        public int AdultsNumber { get; set; }

        public int ChildrenNumber { get; set; }
    }
}
