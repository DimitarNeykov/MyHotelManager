namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task CreateAsync(int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string description);

        IEnumerable<T> GetAll<T>(string userId);

        Task Delete(string reservationId);
    }
}
