namespace MyHotelManager.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using MyHotelManager.Services.Mapping;
    using Xunit;

    public class RoomTypeServiceTest
    {
        [Fact]
        public async Task TestGetAllRoomTypes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("RoomTypes");

            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(new ApplicationDbContext(options.Options));

            await roomTypeRepository.AddAsync(new RoomType { Name = "Double" });
            await roomTypeRepository.AddAsync(new RoomType { Name = "Single" });
            await roomTypeRepository.SaveChangesAsync();

            var roomTypeService = new RoomTypesService(roomTypeRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestRoomType).Assembly);
            var roomTypes = roomTypeService.GetAll<MyTestRoomType>();

            Assert.Equal(2, roomTypes.Count());
        }

        public class MyTestRoomType : IMapFrom<RoomType>
        {
            public string Name { get; set; }
        }
    }
}
