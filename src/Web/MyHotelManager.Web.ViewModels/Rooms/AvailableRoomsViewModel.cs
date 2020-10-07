namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;

    public class AvailableRoomsViewModel
    {
        [Required]
        public DateTime? From { get; set; }

        [Required]
        public DateTime? To { get; set; }

        public IEnumerable<RoomViewModel> Rooms { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
