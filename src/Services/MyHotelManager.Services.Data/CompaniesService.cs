namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public CompaniesService(IDeletableEntityRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task CreateAsync(string name, string bulstat, string phoneNumber, string email, int cityId, string address, string userId)
        {
            var company = new Company
            {
                Name = name,
                Bulstat = bulstat,
                PhoneNumber = phoneNumber,
                Email = email,
                CityId = cityId,
                Address = address,
                UserId = userId,
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            var query = this.companyRepository
                .All()
                .Where(c => c.UserId == userId)
                .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
