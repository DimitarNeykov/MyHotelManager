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

    public class GenderServiceTest
    {
        [Fact]
        public async Task TestGetAllGenders()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Gender");

            var genderRepository = new EfDeletableEntityRepository<Gender>(new ApplicationDbContext(options.Options));

            await genderRepository.AddAsync(new Gender { Name = "Male" });
            await genderRepository.AddAsync(new Gender { Name = "Female" });
            await genderRepository.SaveChangesAsync();

            var genderService = new GendersService(genderRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestGender).Assembly);
            var gender = genderService.GetAll<MyTestGender>();

            Assert.Equal(2, gender.Count());
        }

        public class MyTestGender : IMapFrom<Gender>
        {
            public string Name { get; set; }
        }
    }
}
