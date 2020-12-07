namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int MaxAdultCount { get; set; }

        public int MaxChildCount { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string RoomTypeName { get; set; }
    }
}
