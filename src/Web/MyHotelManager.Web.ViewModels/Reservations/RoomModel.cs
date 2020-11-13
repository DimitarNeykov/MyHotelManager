﻿namespace MyHotelManager.Web.ViewModels.Reservations
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int MaxAdultCount { get; set; }

        public int MaxChildCount { get; set; }

        public int HotelId { get; set; }

        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }
    }
}
