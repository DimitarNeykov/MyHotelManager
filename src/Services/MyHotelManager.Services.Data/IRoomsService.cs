namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomsService
    {
        Task CreateAsync(int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId);

        IEnumerable<T> GetAll<T>(string userId);

        IEnumerable<T> GetAllRoomsForCleaningToday<T>(int hotelId);

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int roomId);

        IEnumerable<T> AvailableRooms<T>(string userId, DateTime arrivalDate, DateTime returnDate);

        Task<IEnumerable<T>> AvailableRoomsWithReservationRoomAsync<T>(string userId, DateTime arrivalDate, DateTime returnDate, string reservationId);

        Task UpdateAsync(int roomId, int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description);
    }
}
