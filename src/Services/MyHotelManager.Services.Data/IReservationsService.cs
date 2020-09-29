namespace MyHotelManager.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task CreateAsync(int roomId, DateTime bookDate, DateTime arrivalDate, DateTime returnDate, string firstName, string lastName, string description);
    }
}
