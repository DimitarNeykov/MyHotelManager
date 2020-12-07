﻿namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationCreateInputModel : IMapTo<Reservation>
    {
        public RoomViewModel Room { get; set; }

        public string ArrivalDate { get; set; }

        public string ShortArrivalDate => Convert.ToDateTime(this.ArrivalDate).ToString("dd.MM.yyyy");

        public string ReturnDate { get; set; }

        public string ShortReturnDate => Convert.ToDateTime(this.ReturnDate).ToString("dd.MM.yyyy");

        public int Nights => (int)(Convert.ToDateTime(this.ReturnDate) - Convert.ToDateTime(this.ArrivalDate)).TotalDays;

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal CustomPrice { get; set; }

        public decimal AllPrice => this.Nights * this.Room.Price;

        [DisplayName("Breakfast")]
        public bool HasBreakfast { get; set; }

        [DisplayName("Lunch")]
        public bool HasLunch { get; set; }

        [DisplayName("Dinner")]
        public bool HasDinner { get; set; }

        public string Description { get; set; }

        public ReservedByGuestInputModel ReservedByGuest { get; set; }
    }
}
