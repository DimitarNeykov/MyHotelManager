namespace MyHotelManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelsService : IHotelsService
    {
        private readonly IDeletableEntityRepository<Hotel> hotelRepository;

        public HotelsService(IDeletableEntityRepository<Hotel> hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task CreateAsync(string name, int cityId, string address, int starsId, int cleaningPerDays, ApplicationUser user)
        {
            var hotel = new Hotel
            {
                Name = name,
                CityId = cityId,
                Address = address,
                StarsId = starsId,
                CleaningPerDays = cleaningPerDays,
            };

            hotel.Users.Add(user);

            await this.hotelRepository.AddAsync(hotel);
            await this.hotelRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdWithDeletedAsync<T>(int id)
        {
            var hotel = await this.hotelRepository
                .AllWithDeleted()
                .Include(h => h.Rooms)
                .ThenInclude(r => r.Reservations)
                .ThenInclude(r => r.Guests)
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return hotel;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var hotel = await this.hotelRepository
                .All()
                .Include(x => x.City)
                .Include(x => x.Company)
                .ThenInclude(x => x.Hotels)
                .Include(x => x.Company)
                .ThenInclude(x => x.City)
                .Include(x => x.Users)
                .ThenInclude(u => u.Gender)
                .Include(h => h.Rooms)
                .ThenInclude(r => r.Reservations)
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return hotel;
        }

        public async Task UpdateAsync(int hotelId, string name, int cityId, string address, int starsId, int cleaningPerDays)
        {
            var hotel = await this.hotelRepository
                .All()
                .FirstOrDefaultAsync(h => h.Id == hotelId);

            hotel.Name = name;
            hotel.CityId = cityId;
            hotel.Address = address;
            hotel.StarsId = starsId;
            hotel.CleaningPerDays = cleaningPerDays;

            await this.hotelRepository.SaveChangesAsync();
        }

        public async Task SetPayTrue(int? hotelId)
        {
            var hotel = this.hotelRepository.All().FirstOrDefault(x => x.Id == hotelId);

            if (hotel != null)
            {
                hotel.IsPay = true;
            }

            await this.hotelRepository.SaveChangesAsync();
        }
    }
}
