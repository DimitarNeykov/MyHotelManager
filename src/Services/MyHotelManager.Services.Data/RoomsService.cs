namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomRepository;

        public RoomsService(IDeletableEntityRepository<Room> roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task CreateAsync(string number, int roomTypeId, decimal price, string description, int hotelId)
        {
            var room = new Room
            {
                Number = number,
                RoomTypeId = roomTypeId,
                Price = price,
                Description = description,
                HotelId = hotelId,
            };

            await this.roomRepository.AddAsync(room);
            await this.roomRepository.SaveChangesAsync();
        }
    }
}
