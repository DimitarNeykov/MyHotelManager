namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomsService
    {
        Task CreateAsync(string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId);

        IEnumerable<T> GetAll<T>(string userId);

        T GetById<T>(int id);

        IEnumerable<T> AvailableRooms<T>(string userId, DateTime from, DateTime to);

        IEnumerable<T> AvailableRoomsWithReservationRoom<T>(string userId, DateTime from, DateTime to, string reservationId);
    }
}
