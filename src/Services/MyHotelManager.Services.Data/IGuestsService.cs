namespace MyHotelManager.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public interface IGuestsService
    {
        Task CreateAsync(string firstName, string lastName, int genderId, string phoneNumber, int? cityId,
            int? countryId, string identificationNumber, string uniqueNumberForeigner, string documentNumber, DateTime dateOfExpiry, DateTime dateOfIssue,
            string reservationId);

        Task<T> GetByIdAsync<T>(string id);

        Task UpdateAsync(string id, string firstName, string lastName, int genderId, string phoneNumber, int? cityId,
            int? countryId, string identificationNumber, string uniqueNumberForeigner, string documentNumber, DateTime? dateOfExpiry, DateTime? dateOfIssue);
    }
}
