namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationCreateInputModel : IMapTo<Reservation>
    {
        public int RoomId { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Description { get; set; }

        public ReservationGuestInfoInputModel GuestInfo { get; set; }
    }
}
