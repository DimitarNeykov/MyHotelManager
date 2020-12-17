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

    public class CountryServiceTest
    {
        [Fact]
        public async Task TestGetAllCountries()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Countries");

            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            await countryRepository.AddAsync(new Country { Name = "Bulgaria" });
            await countryRepository.AddAsync(new Country { Name = "Turkey" });
            await countryRepository.AddAsync(new Country { Name = "Russia" });
            await countryRepository.SaveChangesAsync();

            var countryService = new CountriesService(countryRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCountry).Assembly);
            var countries = countryService.GetAll<MyTestCountry>();

            Assert.Equal(3, countries.Count());
        }

        public class MyTestCountry : IMapFrom<Country>
        {
            public string Name { get; set; }
        }
    }
}
