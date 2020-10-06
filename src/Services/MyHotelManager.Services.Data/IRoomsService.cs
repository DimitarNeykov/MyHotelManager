namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IRoomsService
    {
        Task CreateAsync(string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId);

        IEnumerable<T> GetAll<T>(string userId);

        Room GetById<T>(int id);

        IEnumerable<T> AvailableRooms<T>(string userId, DateTime from, DateTime to);
    }
}
