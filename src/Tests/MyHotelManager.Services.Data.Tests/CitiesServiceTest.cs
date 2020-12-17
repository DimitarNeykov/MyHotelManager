namespace MyHotelManager.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using MyHotelManager.Services.Mapping;
    using Xunit;

    public class CitiesServiceTest
    {
        [Fact]
        public async Task TestGetAllCities()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Cities");

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            await cityRepository.AddAsync(new City { Name = "София" });
            await cityRepository.AddAsync(new City { Name = "Стара Загора" });
            await cityRepository.AddAsync(new City { Name = "Пловдив" });
            await cityRepository.SaveChangesAsync();

            var cityService = new CitiesService(cityRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCity).Assembly);
            var cities = cityService.GetAll<MyTestCity>();

            Assert.Equal(3, cities.Count());
        }

        public class MyTestCity : IMapFrom<City>
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}
