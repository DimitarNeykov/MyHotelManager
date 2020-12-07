namespace MyHotelManager.Web.ViewModels.Reservations
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int MaxAdultCount { get; set; }

        public int MaxChildCount { get; set; }

        public decimal Price { get; set; }

        public string RoomTypeName { get; set; }

        public string Description { get; set; }

        public string Floor { get; set; }
    }
}
