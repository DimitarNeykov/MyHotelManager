using System;
using System.Collections.Generic;
using System.Linq;
using MyHotelManager.Data.Models;
using MyHotelManager.Services.Mapping;

namespace MyHotelManager.Web.ViewModels.Hotels
{
    public class HotelDashboardViewModel : IMapFrom<Hotel>
    {
        public string Name { get; set; }

        public int RoomsCount { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public int AvailableRoomsCount { get; set; }

        public int OccupiedRoomsCount => this.RoomsCount - this.AvailableRoomsCount;

        public int ReservationsCountForThisYear => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.CancelDate == null));

        public int ReservationsCountForOneYearEarly => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year - 1 && r.CancelDate == null));

        public int ReservationsCountForTwoYearEarly => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year - 2 && r.CancelDate == null));
    }
}
