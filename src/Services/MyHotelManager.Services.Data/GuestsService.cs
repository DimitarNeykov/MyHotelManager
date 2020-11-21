using Microsoft.EntityFrameworkCore;

namespace MyHotelManager.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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

        public T GetById<T>(string id)
        {
            var guest = this.guestRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return guest;
        }

        public async Task UpdateAsync(string id, string firstName, string lastName, int genderId, string phoneNumber, int? cityId, int? countryId, string UCN, string PNF, string documentNumber, DateTime? dateOfExpiry, DateTime? dateOfIssue)
        {
            var guest = await this.guestRepository.All().FirstOrDefaultAsync(x => x.Id == id);

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
            guest.UCN = UCN;
            guest.PNF = PNF;
            guest.DocumentNumber = documentNumber;
            guest.DateOfExpiry = dateOfExpiry;
            guest.DateOfIssue = dateOfIssue;

            await this.guestRepository.SaveChangesAsync();
        }
    }
}
