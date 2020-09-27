namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class GuestReservation : BaseDeletableModel<int>
    {
        public string GuestId { get; set; }

        public Guest Guest { get; set; }

        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
