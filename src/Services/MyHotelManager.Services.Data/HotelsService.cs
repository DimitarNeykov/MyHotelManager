namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

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
    }
}
