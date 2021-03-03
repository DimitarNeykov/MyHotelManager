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

    public class ReservationsServiceTest
    {
        [Fact]
        public async Task TestCreateReservation()
        {
            var reservations = new List<Reservation>();
            var mockReservationRepo = new Mock<IDeletableEntityRepository<Reservation>>();
            mockReservationRepo.Setup(x => x.All()).Returns(reservations.AsQueryable());
            mockReservationRepo.Setup(x => x.AddAsync(It.IsAny<Reservation>())).Callback(
                (Reservation reservation) => reservations.Add(reservation));

            var service = new ReservationsService(mockReservationRepo.Object);

            await service.CreateAsync("0888989844", Guid.NewGuid().ToString(), 1, DateTime.UtcNow, DateTime.UtcNow, 2,
                0, "Petar", "Stoqnov", null, 10.00M, true, false, false);

            Assert.Single(reservations);
        }

        [Fact]
        public async Task TestDeleteReservation()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));
            var reservationId = Guid.NewGuid().ToString();

            await reservationRepository.AddAsync(new Reservation() { Id = reservationId });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);

            await reservationsService.DeleteAsync(reservationId, Guid.NewGuid().ToString());

            Assert.Equal(0, reservationRepository.All().Count());
        }

        [Fact]
        public async Task TestDeleteReservationIsInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));
            var reservationId = Guid.NewGuid().ToString();

            await reservationRepository.AddAsync(new Reservation() { Id = reservationId });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);

            await reservationsService.DeleteAsync(reservationId, Guid.NewGuid().ToString());

            Assert.Equal(1, reservationRepository.AllWithDeleted().Count());
        }

        [Fact]
        public async Task TestGetAllReservationsFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await reservationRepository.AddAsync(new Reservation { Id = Guid.NewGuid().ToString(),  Room = new Room { HotelId = 1 } });
            await reservationRepository.AddAsync(new Reservation { Id = Guid.NewGuid().ToString(), Room = new Room { HotelId = 1 } });
            await reservationRepository.AddAsync(new Reservation { Id = Guid.NewGuid().ToString(), Room = new Room { HotelId = 2 } });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservations = reservationsService.GetAll<MyTestReservation>(1);

            Assert.Equal(2, reservations.Count());
        }

        [Fact]
        public async Task TestGetAllReservationsWithBreakfastFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasBreakfast = true,
                Room = new Room
                {
                    Number = "100",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasBreakfast = false,
                Room = new Room
                {
                    Number = "200",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservations = reservationsService.GetActiveReservationsWithBreakfast<MyTestReservation>(1);

            Assert.Single(reservations);
        }

        [Fact]
        public async Task TestGetAllReservationsWithLunchFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasLunch = true,
                Room = new Room
                {
                    Number = "100",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasLunch = false,
                Room = new Room
                {
                    Number = "200",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservations = reservationsService.GetActiveReservationsWithLunch<MyTestReservation>(1);

            Assert.Single(reservations);
        }

        [Fact]
        public async Task TestGetAllReservationsWithDinnerFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasDinner = true,
                Room = new Room
                {
                    Number = "100",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                HasDinner = false,
                Room = new Room
                {
                    Number = "200",
                    HotelId = 1,
                },
                ReturnDate = DateTime.UtcNow.AddDays(5),
                ArrivalDate = DateTime.UtcNow.AddDays(-5),
            });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservations = reservationsService.GetActiveReservationsWithDinner<MyTestReservation>(1);

            Assert.Single(reservations);
        }

        [Fact]
        public async Task TestGetAllDeletedReservations()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Room = new Room
                {
                    HotelId = 1,
                },
                IsDeleted = true,
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Room = new Room
                {
                    HotelId = 1,
                },
                IsDeleted = false,
            });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservations = reservationsService.GetAllDeleted<MyTestReservation>(1);

            Assert.Single(reservations);
        }

        [Fact]
        public async Task TestGetByIdReservationFromHotel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            var reservationId = Guid.NewGuid().ToString();

            await reservationRepository.AddAsync(new Reservation
            {
                Id = reservationId,
                Room = new Room
                {
                    Number = "100",
                    HotelId = 1,
                },
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Room = new Room
                {
                    Number = "200",
                    HotelId = 1,
                },
            });

            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Room = new Room
                {
                    Number = "300",
                    HotelId = 2,
                },
            });

            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservation = await reservationsService.GetByIdAsync<MyTestReservation>(reservationId);

            Assert.Equal("100", reservation.RoomNumber);
        }

        [Fact]
        public async Task TestGetDeletedByIdReservationFromHotelWithDeletedUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));

            var reservationId = Guid.NewGuid().ToString();

            await reservationRepository.AddAsync(new Reservation
            {
                Id = reservationId,
                Room = new Room
                {
                    Number = "100",
                    HotelId = 1,
                },
                Creator = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    IsDeleted = true,
                },
            });
            await reservationRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Room = new Room
                {
                    Number = "300",
                    HotelId = 2,
                },
                Creator = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    IsDeleted = true,
                },
            });

            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestReservation).Assembly);
            var reservation = await reservationsService.GetDeletedByIdAsync<MyTestReservation>(reservationId);

            Assert.Equal("100", reservation.RoomNumber);
        }

        [Fact]
        public async Task TestEditReservation()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservationRepository = new EfDeletableEntityRepository<Reservation>(new ApplicationDbContext(options.Options));
            var reservationId = Guid.NewGuid().ToString();

            var guests = new List<Guest>();
            var guestOne = new Guest
            {
                FirstName = "Petar",
                LastName = "Stoqnov",
                PhoneNumber = "0888888888",
                CreatedOn = DateTime.UtcNow,
            };
            var guestTwo = new Guest
            {
                FirstName = "Dimitar",
                LastName = "Neykov",
                PhoneNumber = "0888888888",
                CreatedOn = DateTime.UtcNow.AddDays(-1),
            };

            guests.Add(guestOne);
            guests.Add(guestTwo);

            await reservationRepository.AddAsync(new Reservation
            {
                Id = reservationId,
                ArrivalDate = DateTime.UtcNow,
                Guests = guests,
            });
            await reservationRepository.SaveChangesAsync();

            var reservationsService = new ReservationsService(reservationRepository);

            await reservationsService.UpdateAsync(Guid.NewGuid().ToString(), reservationId, 1,
                DateTime.UtcNow.AddDays(1), DateTime.UtcNow, 1, 1, "Georgi",
                "Petrov", "0888989844", null, 10.00M, true, false, false);

            var result = reservationRepository.All().FirstOrDefault();

            Assert.Equal(DateTime.UtcNow.AddDays(1).Date, result.ArrivalDate.Date);
        }

        public class MyTestReservation : IMapFrom<Reservation>
        {
            public string Id { get; set; }

            public string RoomNumber { get; set; }
        }
    }
}
