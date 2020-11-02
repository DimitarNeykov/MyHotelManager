namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationUpdateViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public Room Room { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public string Description { get; set; }

        public ICollection<GuestReservation> GuestsReservations { get; set; }
    }
}
