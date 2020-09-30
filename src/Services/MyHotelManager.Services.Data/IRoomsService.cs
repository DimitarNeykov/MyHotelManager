namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IRoomsService
    {
        Task CreateAsync(string number, int roomTypeId, decimal price, string description, int hotelId);

        IEnumerable<T> GetAll<T>(string userId);

        Room GetById<T>(int id);

        IEnumerable<T> GetFromPeriod<T>(string userId, DateTime from, DateTime to);
    }
}
