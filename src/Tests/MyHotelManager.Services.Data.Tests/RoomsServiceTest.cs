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

    public class RoomsServiceTest
    {
        [Fact]
        public async Task TestCreateRoom()
        {
            var rooms = new List<Room>();
            var mockRoomRepo = new Mock<IDeletableEntityRepository<Room>>();
            mockRoomRepo.Setup(x => x.All()).Returns(rooms.AsQueryable());
            mockRoomRepo.Setup(x => x.AddAsync(It.IsAny<Room>())).Callback(
                (Room room) => rooms.Add(room));

            var mockReservationRepo = new Mock<IDeletableEntityRepository<Reservation>>();

            var service = new RoomsService(mockRoomRepo.Object, mockReservationRepo.Object);

            await service.CreateAsync(1, "100", 1, 100.00M, 2, 0, null, 1);

            Assert.Single(rooms);
        }

        [Fact]
        public async Task TestGetAllRoomFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var roomRepository = new EfDeletableEntityRepository<Room>(new ApplicationDbContext(options.Options));
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await roomRepository.AddAsync(new Room { HotelId = 1 });
            await roomRepository.AddAsync(new Room { HotelId = 1 });
            await roomRepository.AddAsync(new Room { HotelId = 2 });
            await roomRepository.SaveChangesAsync();

            var roomsService = new RoomsService(roomRepository, reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestRoom).Assembly);
            var rooms = roomsService.GetAll<MyTestRoom>(1);

            Assert.Equal(2, rooms.Count());
        }

        [Fact]
        public async Task TestGetRoomByIdFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var roomRepository = new EfDeletableEntityRepository<Room>(new ApplicationDbContext(options.Options));
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await roomRepository.AddAsync(new Room
            {
                Id = 1,
                Number = "100",
                HotelId = 1,
            });
            await roomRepository.AddAsync(new Room
            {
                Id = 2,
                Number = "200",
                HotelId = 2,
            });

            await roomRepository.SaveChangesAsync();

            var roomsService = new RoomsService(roomRepository, reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestRoom).Assembly);
            var room = await roomsService.GetByIdAsync<MyTestRoom>(1);

            Assert.Equal("100", room.Number);
        }

        [Fact]
        public async Task TestGetAllAvailableRoomFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var roomRepository = new EfDeletableEntityRepository<Room>(new ApplicationDbContext(options.Options));
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            var reservationsList = new List<Reservation>();

            var reservation = new Reservation
            {
                ArrivalDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(2),
            };

            reservationsList.Add(reservation);

            await roomRepository.AddAsync(new Room { HotelId = 1 });
            await roomRepository.AddAsync(new Room
            {
                HotelId = 1,
                Reservations = reservationsList,
            });
            await roomRepository.AddAsync(new Room { HotelId = 2 });
            await roomRepository.SaveChangesAsync();

            var roomsService = new RoomsService(roomRepository, reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestRoom).Assembly);
            var rooms = roomsService.AvailableRooms<MyTestRoom>(1, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));

            Assert.Single(rooms);
        }

        [Fact]
        public async Task TestDeleteRoom()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var roomRepository = new EfDeletableEntityRepository<Room>(new ApplicationDbContext(options.Options));
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await roomRepository.AddAsync(new Room { Id = 1 });
            await roomRepository.SaveChangesAsync();

            var roomsService = new RoomsService(roomRepository, reservationRepository);

            await roomsService.DeleteAsync(1);

            Assert.Equal(0, reservationRepository.All().Count());
        }

        [Fact]
        public async Task TestEditReservation()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));
            var roomRepository = new EfDeletableEntityRepository<Room>(new ApplicationDbContext(options.Options));

            await roomRepository.AddAsync(new Room
            {
                Id = 1,
                Number = "100",
            });

            await roomRepository.SaveChangesAsync();

            var roomsService = new RoomsService(roomRepository, reservationRepository);

            await roomsService.UpdateAsync(1, 2, "105B", 1, 100.00M, 2, 0, null);

            var result = roomRepository.All().FirstOrDefault();

            Assert.Equal("105B", result.Number);
        }

        public class MyTestRoom : IMapFrom<Room>
        {
            public string Id { get; set; }

            public string Number { get; set; }
        }
    }
}
