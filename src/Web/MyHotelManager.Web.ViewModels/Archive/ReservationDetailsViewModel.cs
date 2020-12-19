namespace MyHotelManager.Web.ViewModels.Archive
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationDetailsViewModel : IMapFrom<Reservation>
    {
        public RoomViewModel Room { get; set; }

        [DisplayName("Book Date")]
        public DateTime BookDate { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

        public int Nights => (this.ReturnDate - this.ArrivalDate).Days;

        [DisplayName("Cancel Date")]
        public DateTime CancelDate { get; set; }

        public CreatorViewModel Creator { get; set; }

        public EditorViewModel Editor { get; set; }

        [DisplayName("Adult Count")]
        public int AdultCount { get; set; }

        [DisplayName("Child Count")]
        public int ChildCount { get; set; }

        [DisplayName("Custom Price")]
        public decimal CustomPrice { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool HasBreakfast { get; set; }

        public bool HasLunch { get; set; }

        public bool HasDinner { get; set; }

        public IEnumerable<GuestViewModel> Guests { get; set; }
    }
}
