namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public string Number { get; set; }

        public string Description { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        public decimal Price { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
