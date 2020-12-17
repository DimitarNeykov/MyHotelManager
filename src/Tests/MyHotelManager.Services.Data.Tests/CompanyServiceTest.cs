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

    public class CompanyServiceTest
    {
        [Fact]
        public async Task WhenCreateCompanyThatDoesNotExistDatabase()
        {
            var companies = new List<Company>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Company>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(companies.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Company>())).Callback(
                (Company company) => companies.Add(company));

            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();

            var service = new CompaniesService(mockCompanyRepo.Object, mockHotelRepo.Object);

            await service.CreateAsync("NeykovEOOD", "131071587", "0888989844", "test@gmail.com", 1, "Първи май №2", 1);

            Assert.Single(companies);
        }

        [Fact]
        public async Task WhenCreateCompanyThatExistInDatabase()
        {
            var companies = new List<Company>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Company>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(companies.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Company>())).Callback(
                (Company company) => companies.Add(company));

            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();

            var service = new CompaniesService(mockCompanyRepo.Object, mockHotelRepo.Object);

            await service.CreateAsync("NeykovEOOD", "131071587", "0888989844", "test@gmail.com", 1, "Първи май №2", 1);
            await service.CreateAsync("NeykovEOOD", "131071587", "0888989844", "test@gmail.com", 1, "Първи май №2", 1);

            Assert.Single(companies);
        }

        [Fact]
        public async Task WhenEditCompanyWhereBulstatIsEqual()
        {
            var companies = new List<Company>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Company>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(companies.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Company>())).Callback(
                (Company company) => companies.Add(company));

            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();

            var service = new CompaniesService(mockCompanyRepo.Object, mockHotelRepo.Object);

            var company = new Company
            {
                Id = 1,
                Bulstat = "131071587",
            };

            companies.Add(company);
            await service.EditAsync(1, 1, "NeykovEOOD", "131071587", "0888989844", "test@gmail.com", 1, "Първи май №2");

            Assert.Single(companies);
        }

        [Fact]
        public async Task WhenEditCompanyWhereBulstatIsDifferent()
        {
            var companies = new List<Company>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Company>>();
            mockCompanyRepo.Setup(x => x.All()).Returns(companies.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Company>())).Callback(
                (Company company) => companies.Add(company));

            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();

            var service = new CompaniesService(mockCompanyRepo.Object, mockHotelRepo.Object);

            var company = new Company
            {
                Id = 1,
                Bulstat = "131071587",
            };

            companies.Add(company);
            await service.EditAsync(1, 1, "NeykovEOOD", "131071589", "0888989844", "test@gmail.com", 1, "Първи май №2");

            Assert.Equal(2, companies.Count);
        }

        [Fact]
        public async Task WhenEditCompanyWhereBulstatIsDifferentButEqualWithAnotherOne()
        {
            var companies = new List<Company>();
            var mockCompanyRepo = new Mock<IDeletableEntityRepository<Company>>();

            mockCompanyRepo.Setup(x => x.All()).Returns(companies.AsQueryable());
            mockCompanyRepo.Setup(x => x.AddAsync(It.IsAny<Company>())).Callback(
                (Company company) => companies.Add(company));

            var mockHotelRepo = new Mock<IDeletableEntityRepository<Hotel>>();

            var service = new CompaniesService(mockCompanyRepo.Object, mockHotelRepo.Object);

            var firstCompany = new Company
            {
                Id = 1,
                Address = "Първи май №2",
                Bulstat = "131071587",
                CityId = 1,
                Email = "test@gmail.com",
            };

            var secondCompany = new Company
            {
                Id = 1,
                Address = "Първи май №2",
                Bulstat = "131071589",
                CityId = 1,
                Email = "test@gmail.com",
            };

            companies.Add(firstCompany);
            companies.Add(secondCompany);
            await service.EditAsync(1, 1, "NeykovEOOD", "131071589", "0888989844", "test@gmail.com", 1, "Първи май №2");

            Assert.Equal(2, companies.Count);
        }

        [Fact]
        public async Task TestGetByIdCompany()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var companyRepository = new EfDeletableEntityRepository<Company>(new ApplicationDbContext(options.Options));
            var hotelRepository = new EfDeletableEntityRepository<Hotel>(new ApplicationDbContext(options.Options));

            await companyRepository.AddAsync(new Company { Id = 1, Bulstat = "131071587" });
            await companyRepository.SaveChangesAsync();

            var companyService = new CompaniesService(companyRepository, hotelRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCompany).Assembly);
            var company = await companyService.GetByIdAsync<MyTestCompany>(1);

            Assert.Equal("131071587", company.Bulstat);
        }

        public class MyTestCompany : IMapFrom<Company>
        {
            public string Id { get; set; }

            public string Bulstat { get; set; }
        }
    }
}
