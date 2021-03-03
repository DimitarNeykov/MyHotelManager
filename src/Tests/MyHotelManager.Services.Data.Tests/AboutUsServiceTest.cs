namespace MyHotelManager.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using MyHotelManager.Services.Mapping;
    using Xunit;

    public class AboutUsServiceTest
    {
        [Fact]
        public async Task TestGetHotelById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var aboutUsRepository = new EfDeletableEntityRepository<AboutUs>(new ApplicationDbContext(options.Options));

            await aboutUsRepository.AddAsync(new AboutUs { Email = "test@abv.bg", Address = "Test address", Phone = "0888989844", LocationUrlForGoogleMaps = "TestUrl", LocationUrlForOpenStreetMap = "TestUrl" });
            await aboutUsRepository.SaveChangesAsync();

            var aboutUsService = new AboutUsService(aboutUsRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestAboutUs).Assembly);

            var result = await aboutUsService.GetInformationAsync<MyTestAboutUs>();

            Assert.Equal("test@abv.bg", result.Email);
        }

        public class MyTestAboutUs : IMapFrom<AboutUs>
        {
            public string Email { get; set; }

            public string Phone { get; set; }

            public string Address { get; set; }

            public string LocationUrlForGoogleMaps { get; set; }

            public string LocationUrlForOpenStreetMap { get; set; }
        }
    }
}
