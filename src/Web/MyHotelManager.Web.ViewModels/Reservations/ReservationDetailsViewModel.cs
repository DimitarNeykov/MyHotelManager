namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationDetailsViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public RoomViewModel Room { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Nights => (this.ReturnDate - this.ArrivalDate).Days;

        public DateTime ModifiedOn { get; set; }

        public CreatorViewModel Creator { get; set; }

        public EditorViewModel Editor { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal CustomPrice { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool HasBreakfast { get; set; }

        public bool HasLunch { get; set; }

        public bool HasDinner { get; set; }

        public IEnumerable<GuestViewModel> Guests { get; set; }
    }
}
