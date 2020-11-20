using MyHotelManager.Data.Common.Repositories;
using MyHotelManager.Data.Models;

namespace MyHotelManager.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<Guest> guestRepository;

        public GuestsService(IDeletableEntityRepository<Guest> guestRepository)
        {
            this.guestRepository = guestRepository;
        }

        public async Task CreateAsync(string firstName, string lastName, int genderId, string phoneNumber, int? cityId, int? countryId, string UCN, string PNF, string documentNumber, DateTime dateOfExpiry, DateTime dateOfIssue, string reservationId)
        {
            Guest guest;
            if (PNF == null)
            {
                guest = new Guest
                {
                    CityId = cityId,
                    DateOfExpiry = dateOfExpiry,
                    DateOfIssue = dateOfIssue,
                    DocumentNumber = documentNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    GenderId = genderId,
                    PhoneNumber = phoneNumber,
                    UCN = UCN,
                    ReservationId = reservationId,
                };
            }
            else
            {
                guest = new Guest
                {
                    CountryId = countryId,
                    DateOfExpiry = dateOfExpiry,
                    DateOfIssue = dateOfIssue,
                    DocumentNumber = documentNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    GenderId = genderId,
                    PhoneNumber = phoneNumber,
                    PNF = PNF,
                    ReservationId = reservationId,
                };
            }

            await this.guestRepository.AddAsync(guest);
            await this.guestRepository.SaveChangesAsync();
        }
    }
}
