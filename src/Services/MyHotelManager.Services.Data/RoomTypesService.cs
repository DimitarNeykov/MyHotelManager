namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomTypesService : IRoomTypesService
    {
        private readonly IDeletableEntityRepository<RoomType> roomTypesRepository;

        public RoomTypesService(IDeletableEntityRepository<RoomType> roomTypesRepository)
        {
            this.roomTypesRepository = roomTypesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var roomTypes = this.roomTypesRepository
                .All()
                .To<T>()
                .ToList();

            return roomTypes;
        }
    }
}
