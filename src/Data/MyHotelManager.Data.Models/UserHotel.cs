namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class UserHotel : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
