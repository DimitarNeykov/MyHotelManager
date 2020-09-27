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

        public async Task CreateAsync(string name, int cityId, string address, int starsId, int companyId, ApplicationUser user)
        {
            var hotel = new Hotel
            {
                Name = name,
                CityId = cityId,
                Address = address,
                StarsId = starsId,
                CompanyId = companyId,
            };

            var userHotel = new UserHotel
            {
                User = user,
                Hotel = hotel,
            };

            hotel.UsersHotels.Add(userHotel);

            await this.hotelRepository.AddAsync(hotel);
            await this.hotelRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            var hotel = this.hotelRepository
                .All()
                .Where(h => h.UsersHotels.Any(uh => uh.UserId == userId));

            return hotel.To<T>();
        }

        public T GetById<T>(int id)
        {
            var post = this.hotelRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return post;
        }
    }
}
