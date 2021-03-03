namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelDashboardViewModel : IMapFrom<Hotel>
    {
        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public int AvailableRoomsCount { get; set; }

        public int OccupiedRoomsCount => this.Rooms.Count(r => r.IsDeleted == false) - this.AvailableRoomsCount;

        public double ReservationsForToday => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ArrivalDate.Date == DateTime.Now.Date && r.CancelDate == null && r.IsDeleted == false));

        public double ArrivedReservationsForToday => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ArrivalDate.Date == DateTime.Now.Date && r.CancelDate == null && r.IsDeleted == false &&
                        r.Guests.Any(x => x.UniqueNumberForeigner != null || x.IdentificationNumber != null)));

        public double PercentageOfArrivedReservations =>
            (this.ArrivedReservationsForToday / this.ReservationsForToday) * 100;

        public int ReservationsCountForThisYear => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.CancelDate == null && r.IsDeleted == true));

        public int ReservationsCountForOneYearEarly => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year - 1 && r.CancelDate == null && r.IsDeleted == true));

        public int ReservationsCountForTwoYearEarly => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year - 2 && r.CancelDate == null && r.IsDeleted == true));

        public int JanuaryReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 1 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int FebruaryReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 2 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int MarchReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 3 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int AprilReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 4 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int MayReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 5 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int JuneReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 6 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int JulyReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 7 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int AugustReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 8 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int SeptemberReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 9 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int OctoberReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 10 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int NovemberReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 11 && r.CancelDate == null &&
                        r.IsDeleted == true));

        public int DecemberReservations => this.Rooms.Sum(r => r.Reservations
            .Count(r => r.ReturnDate.Year == DateTime.Now.Year && r.ReturnDate.Month == 12 && r.CancelDate == null &&
                        r.IsDeleted == true));
    }
}
