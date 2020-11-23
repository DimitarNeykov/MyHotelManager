namespace MyHotelManager.Web.ViewModels.Restaurant
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>
    {
        public Room Room { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public int TotalCount => this.AdultCount + this.ChildCount;

        public ICollection<Guest> Guests { get; set; }
    }
}
