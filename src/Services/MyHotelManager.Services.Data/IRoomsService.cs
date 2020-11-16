﻿namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomsService
    {
        Task CreateAsync(int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId);

        IEnumerable<T> GetAll<T>(string userId);

        T GetById<T>(int id);

        Task Delete(int roomId);

        IEnumerable<T> AvailableRooms<T>(string userId, DateTime arrivalDate, DateTime returnDate);

        IEnumerable<T> AvailableRoomsWithReservationRoom<T>(string userId, DateTime arrivalDate, DateTime returnDate, string reservationId);

        Task UpdateAsync(int roomId, int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description);
    }
}
