namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationUpdateInputModel : IMapTo<Reservation>
    {
        public string Id { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Nights => (int)(this.ReturnDate - this.ArrivalDate).TotalDays;

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal CustomPrice { get; set; }

        public decimal AllPrice { get; set; }

        [DisplayName("Breakfast")]
        public bool HasBreakfast { get; set; }

        [DisplayName("Lunch")]
        public bool HasLunch { get; set; }

        [DisplayName("Dinner")]
        public bool HasDinner { get; set; }

        public string Description { get; set; }

        public ReservedByGuestInputModel ReservedByGuest { get; set; }

        public RoomViewModel Room { get; set; }
    }
}
