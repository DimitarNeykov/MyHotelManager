namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task CreateAsync(string phoneNumber, string userId, int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string description, decimal price, bool hasBreakfast, bool hasLunch, bool hasDinner);

        IEnumerable<T> GetAll<T>(int hotelId);

        Task<T> GetDeletedByIdAsync<T>(string reservationId);

        IEnumerable<T> GetAllDeleted<T>(int hotelId);

        Task<T> GetByIdAsync<T>(string reservationId);

        Task DeleteAsync(string reservationId, string editorId);

        Task UpdateAsync(string userId, string reservationId, int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string phoneNumber, string description, decimal price, bool hasBreakfast, bool hasLunch, bool hasDinner);

        IEnumerable<T> GetActiveReservationsWithBreakfast<T>(int hotelId);

        IEnumerable<T> GetActiveReservationsWithLunch<T>(int hotelId);

        IEnumerable<T> GetActiveReservationsWithDinner<T>(int hotelId);
    }
}
