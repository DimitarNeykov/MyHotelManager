namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class UserHotel : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
