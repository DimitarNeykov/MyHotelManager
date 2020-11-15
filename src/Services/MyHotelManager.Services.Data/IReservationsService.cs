namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task CreateAsync(string phoneNumber, string userId, int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string description, decimal price, bool hasBreakfast, bool hasLunch, bool hasDinner);

        IEnumerable<T> GetAll<T>(string userId);

        T GetById<T>(string reservationId);

        Task Delete(string reservationId);

        Task UpdateAsync(string userId, string reservationId, int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string description, decimal price, bool hasBreakfast, bool hasLunch, bool hasDinner);
    }
}
