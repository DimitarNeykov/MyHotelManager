namespace MyHotelManager.Web.ViewModels.Archive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public Room Room { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime CancelDate { get; set; }

        public ApplicationUser Creator { get; set; }

        public ApplicationUser Editor { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal Price { get; set; }

        public decimal CustomPrice { get; set; }

        public int Nights => (this.ReturnDate - this.ArrivalDate).Days;

        public string Description { get; set; }

        public bool HasBreakfast { get; set; }

        public bool HasLunch { get; set; }

        public bool HasDinner { get; set; }

        public ICollection<Guest> Guests { get; set; }

        public Guest ReservationGuest => this.Guests.OrderBy(x => x.CreatedOn).First();
    }
}
