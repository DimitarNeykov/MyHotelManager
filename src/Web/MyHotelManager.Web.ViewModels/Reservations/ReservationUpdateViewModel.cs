namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationUpdateViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public int RoomId { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Nights => (int)(Convert.ToDateTime(this.ReturnDate) - Convert.ToDateTime(this.ArrivalDate)).TotalDays;

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Breakfast")]
        public bool HasBreakfast { get; set; }

        [DisplayName("Lunch")]
        public bool HasLunch { get; set; }

        [DisplayName("Dinner")]
        public bool HasDinner { get; set; }

        public string Description { get; set; }

        public ICollection<GuestReservation> GuestsReservations { get; set; }
    }
}
