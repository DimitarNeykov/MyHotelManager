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

namespace MyHotelManager.Services.Data.Tests
{
    public class GuestServiceTest
    {
        [Fact]
        public async Task WhenCreateGuestWhoIsForeigner()
        {
            var guests = new List<Guest>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Guest>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(guests.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Guest>())).Callback(
                (Guest guest) => guests.Add(guest));

            var service = new GuestsService(mockCompanyRepo.Object);

            await service.CreateAsync("Dimitar", "Neykov", 1, "0888989844", null, 1, null, "1151541651", "12544545145", DateTime.UtcNow, DateTime.UtcNow, Guid.NewGuid().ToString());

            Assert.Single(guests);
        }

        [Fact]
        public async Task WhenCreateGuestWhoIsBulgarian()
        {
            var guests = new List<Guest>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Guest>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(guests.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Guest>())).Callback(
                (Guest guest) => guests.Add(guest));

            var service = new GuestsService(mockCompanyRepo.Object);

            await service.CreateAsync("Dimitar", "Neykov", 1, "0888989844", 1, null, "1151541651", null, "12544545145", DateTime.UtcNow, DateTime.UtcNow, Guid.NewGuid().ToString());

            Assert.Single(guests);
        }

        [Fact]
        public async Task TestEditUserWithCityId0()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var guestRepository = new EfDeletableEntityRepository<Guest>(new ApplicationDbContext(options.Options));

            var guid = Guid.NewGuid().ToString();

            await guestRepository.AddAsync(new Guest { Id = guid, FirstName = "Petar", LastName = "Stoqnov"});
            await guestRepository.SaveChangesAsync();

            var guestsService = new GuestsService(guestRepository);
            await guestsService.UpdateAsync(guid, "Georgi", "Stoqnov", 1, null, 0, null, null, null, null, null, null);

            Assert.Equal("Georgi", guestRepository.All().FirstOrDefault().FirstName);
        }

        [Fact]
        public async Task TestEditUserWithCountryId0()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var guestRepository = new EfDeletableEntityRepository<Guest>(new ApplicationDbContext(options.Options));

            var guid = Guid.NewGuid().ToString();

            await guestRepository.AddAsync(new Guest { Id = guid, FirstName = "Petar", LastName = "Stoqnov" });
            await guestRepository.SaveChangesAsync();

            var guestsService = new GuestsService(guestRepository);
            await guestsService.UpdateAsync(guid, "Georgi", "Stoqnov", 1, null, null, 0, null, null, null, null, null);

            Assert.Equal("Georgi", guestRepository.All().FirstOrDefault().FirstName);
        }

        [Fact]
        public async Task TestGetByIdGuest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Cities");

            var guestRepository = new EfDeletableEntityRepository<Guest>(new ApplicationDbContext(options.Options));

            var pretarGuid = Guid.NewGuid().ToString();
            var stoqnGuid = Guid.NewGuid().ToString();

            await guestRepository.AddAsync(new Guest { Id = pretarGuid, FirstName = "Petar" });
            await guestRepository.AddAsync(new Guest { Id = stoqnGuid, FirstName = "Stoqn" });
            await guestRepository.SaveChangesAsync();

            var guestsService = new GuestsService(guestRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestGuest).Assembly);
            var guest = await guestsService.GetByIdAsync<MyTestGuest>(pretarGuid);

            Assert.Equal("Petar", guest.FirstName);
        }

        public class MyTestGuest : IMapFrom<Guest>
        {
            public string Id { get; set; }

            public string FirstName { get; set; }
        }
    }
}
