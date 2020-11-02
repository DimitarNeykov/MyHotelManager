namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;

    public class AvailableRoomsViewModel
    {
        public IEnumerable<RoomViewModel> Rooms { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
