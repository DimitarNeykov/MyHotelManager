namespace MyHotelManager.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.GuestsReservations = new HashSet<GuestReservation>();
        }

        public int? RoomId { get; set; }

        public virtual Room Room { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GuestReservation> GuestsReservations { get; set; }
    }
}
