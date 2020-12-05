namespace MyHotelManager.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<Guest> guestRepository;

        public GuestsService(IDeletableEntityRepository<Guest> guestRepository)
        {
            this.guestRepository = guestRepository;
        }

        public async Task CreateAsync(string firstName, string lastName, int genderId, string phoneNumber, int? cityId, int? countryId, string identificationNumber, string uniqueNumberForeigner, string documentNumber, DateTime dateOfExpiry, DateTime dateOfIssue, string reservationId)
        {
            Guest guest;
            if (uniqueNumberForeigner == null)
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
                    IdentificationNumber = identificationNumber,
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
                    UniqueNumberForeigner = uniqueNumberForeigner,
                    ReservationId = reservationId,
                };
            }

            await this.guestRepository.AddAsync(guest);
            await this.guestRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var guest = await this.guestRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return guest;
        }

        public async Task UpdateAsync(string id, string firstName, string lastName, int genderId, string phoneNumber, int? cityId, int? countryId, string identificationNumber, string uniqueNumberForeigner, string documentNumber, DateTime? dateOfExpiry, DateTime? dateOfIssue)
        {
            var guest = await this.guestRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (countryId == 0)
            {
                countryId = null;
            }

            if (cityId == 0)
            {
                cityId = null;
            }

            guest.FirstName = firstName;
            guest.LastName = lastName;
            guest.GenderId = genderId;
            guest.PhoneNumber = phoneNumber;
            guest.CityId = cityId;
            guest.CountryId = countryId;
            guest.IdentificationNumber = identificationNumber;
            guest.UniqueNumberForeigner = uniqueNumberForeigner;
            guest.DocumentNumber = documentNumber;
            guest.DateOfExpiry = dateOfExpiry;
            guest.DateOfIssue = dateOfIssue;

            await this.guestRepository.SaveChangesAsync();
        }
    }
}
