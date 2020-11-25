namespace MyHotelManager.Web.ViewModels.Cleaning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CleaningRoomViewModel : IMapFrom<Room>
    {
        public string Number { get; set; }

        public RoomType RoomType { get; set; }

        public int HotelCleaningPerDays { get; set; }

        public Reservation Reservation => this.Reservations.FirstOrDefault(x =>
            (DateTime.Now.Date - x.ArrivalDate.Date).Days % this.HotelCleaningPerDays == 0 && x.ArrivalDate.Date < DateTime.Now.Date ||
            x.ReturnDate.Date == DateTime.Now.Date);

        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
