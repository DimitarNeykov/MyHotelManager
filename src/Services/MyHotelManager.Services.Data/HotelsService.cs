using Microsoft.EntityFrameworkCore;

namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task CreateAsync(string name, int cityId, string address, int starsId, ApplicationUser user, string imgUrl)
        {
            var hotel = new Hotel
            {
                Name = name,
                CityId = cityId,
                Address = address,
                StarsId = starsId,
            };

            hotel.Users.Add(user);

            await this.hotelRepository.AddAsync(hotel);
            await this.hotelRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var hotel = this.hotelRepository.All()
                .Include(x => x.City)
                .Include(x => x.Company)
                .ThenInclude(x => x.Hotels)
                .Include(x => x.Company)
                .ThenInclude(x => x.City)
                .Include(x => x.Users)
                .ThenInclude(u => u.Gender)
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return hotel;
        }
    }
}
