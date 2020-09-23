namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class GuestReservation : BaseDeletableModel<int>
    {
        public string GuestId { get; set; }

        public virtual Guest Guest { get; set; }

        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
