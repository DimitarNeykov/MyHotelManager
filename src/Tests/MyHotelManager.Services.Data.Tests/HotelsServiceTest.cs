namespace MyHotelManager.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyHotelManager.Data;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using MyHotelManager.Services.Mapping;
    using Xunit;

    public class HotelsServiceTest
    {
        [Fact]
        public async Task TestCreateHotel()
        {
            var hotels = new List<Hotel>();
            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();
            mockHotelRepo.Setup(x => x.All()).Returns(hotels.AsQueryable());
            mockHotelRepo.Setup(x => x.AddAsync(It.IsAny<Hotel>())).Callback(
                (Hotel hotel) => hotels.Add(hotel));

            var service = new HotelsService(mockHotelRepo.Object);

            await service.CreateAsync("Test Name", 1, "Test Address", 1, 2, null);

            Assert.Single(hotels);
        }

        [Fact]
        public async Task TestGetHotelByIdWithDeletedRoom()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var hotelRepository = new EfDeletableEntityRepository<Hotel>(new ApplicationDbContext(options.Options));

            var hotel = new Hotel
            {
                Id = 1,
                Name = "Test Hotel",
            };

            var roomOne = new Room
            {
                Id = 1,
                Number = "100",
                IsDeleted = true,
            };

            var roomTwo = new Room
            {
                Id = 2,
                Number = "200",
                IsDeleted = false,
            };
            hotel.Rooms.Add(roomOne);
            hotel.Rooms.Add(roomTwo);

            await hotelRepository.AddAsync(hotel);
            await hotelRepository.SaveChangesAsync();

            var hotelsService = new HotelsService(hotelRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestHotel).Assembly);
            var result = await hotelsService.GetByIdWithDeletedAsync<MyTestHotel>(1);

            Assert.Equal(2, result.Rooms.Count());
        }

        [Fact]
        public async Task TestGetHotelById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var hotelRepository = new EfDeletableEntityRepository<Hotel>(new ApplicationDbContext(options.Options));

            await hotelRepository.AddAsync(new Hotel { Id = 1, Name = "Test Hotel Name One" });
            await hotelRepository.AddAsync(new Hotel { Id = 2, Name = "Test Hotel Name Two" });
            await hotelRepository.SaveChangesAsync();

            var hotelsService = new HotelsService(hotelRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestHotel).Assembly);

            var result = await hotelsService.GetByIdAsync<MyTestHotel>(1);

            Assert.Equal("Test Hotel Name One", result.Name);
        }

        [Fact]
        public async Task TestUpdateHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var hotelRepository = new EfDeletableEntityRepository<Hotel>(new ApplicationDbContext(options.Options));

            await hotelRepository.AddAsync(new Hotel { Id = 1, Name = "Test Hotel Name One" });
            await hotelRepository.SaveChangesAsync();

            var hotelsService = new HotelsService(hotelRepository);

            await hotelsService.UpdateAsync(1, "Test Hotel", 1, null, 1, 1);

            var result = hotelRepository.All().FirstOrDefault();

            Assert.Equal("Test Hotel", result.Name);
        }

        public class MyTestHotel : IMapFrom<Hotel>
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public IEnumerable<Room> Rooms { get; set; }
        }
    }
}
