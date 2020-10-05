namespace MyHotelManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Hotel> hotelRepository;

        public CompaniesService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Hotel> hotelRepository)
        {
            this.companyRepository = companyRepository;
            this.hotelRepository = hotelRepository;
        }

        public async Task CreateAsync(string name, string bulstat, string phoneNumber, string email, int cityId, string address, ApplicationUser user)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Bulstat == bulstat);

            var hotel = this.hotelRepository.All().FirstOrDefault(h => h.Id == user.HotelId);

            if (company != null)
            {
                company.Hotels.Add(hotel);
                await this.companyRepository.SaveChangesAsync();
                return;
            }

            company = new Company
            {
                Name = name,
                Bulstat = bulstat,
                PhoneNumber = phoneNumber,
                Email = email,
                CityId = cityId,
                Address = address,
            };
            company.Hotels.Add(hotel);

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
        }
    }
}
