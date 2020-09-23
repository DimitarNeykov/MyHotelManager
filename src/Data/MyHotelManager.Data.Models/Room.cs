namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }

        public decimal Price { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
