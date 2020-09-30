namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task CreateAsync(int roomId, DateTime arrivalDate, DateTime returnDate, string firstName, string lastName, string description);

        IEnumerable<T> GetAll<T>(string userId);
    }
}
