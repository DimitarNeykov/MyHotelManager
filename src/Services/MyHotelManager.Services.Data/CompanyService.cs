namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public CompanyService(IDeletableEntityRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task CreateAsync(string name, string bulstat, string phoneNumber, string email, string address, string userId)
        {
            var company = new Company
            {
                Name = name,
                Bulstat = bulstat,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                UserId = userId,
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
        }
    }
}
