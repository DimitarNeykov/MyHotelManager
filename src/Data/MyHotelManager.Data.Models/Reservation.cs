﻿namespace MyHotelManager.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Guests = new HashSet<Guest>();
        }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal Price { get; set; }

        public bool HasBreakfast { get; set; }

        public bool HasLunch { get; set; }

        public bool HasDinner { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public string EditorId { get; set; }

        public ApplicationUser Editor { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
