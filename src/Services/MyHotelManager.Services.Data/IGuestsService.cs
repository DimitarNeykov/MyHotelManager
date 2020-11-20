namespace MyHotelManager.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public interface IGuestsService
    {
        Task CreateAsync(string firstName, string lastName, int genderId, string phoneNumber, int? cityId, int? countryId, string UCN, string PNF, string documentNumber, DateTime dateOfExpiry, DateTime dateOfIssue, string reservationId);
    }
}
