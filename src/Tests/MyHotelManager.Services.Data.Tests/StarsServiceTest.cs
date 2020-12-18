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

    public class StarsServiceTest
    {
        [Fact]
        public async Task TestGetAllStars()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Stars");

            var starRepository = new EfDeletableEntityRepository<Stars>(new ApplicationDbContext(options.Options));

            await starRepository.AddAsync(new Stars { Name = "One" });
            await starRepository.AddAsync(new Stars { Name = "Two" });
            await starRepository.SaveChangesAsync();

            var starsService = new StarsService(starRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestStars).Assembly);
            var stars = starsService.GetAll<MyTestStars>();

            Assert.Equal(2, stars.Count());
        }

        public class MyTestStars : IMapFrom<Stars>
        {
            public string Name { get; set; }
        }
    }
}
