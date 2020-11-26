namespace MyHotelManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

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

        public async Task CreateAsync(string name, string bulstat, string phoneNumber, string email, int cityId, string address, int hotelId)
        {
            var company = await this.companyRepository.All().FirstOrDefaultAsync(c => c.Bulstat == bulstat);

            var hotel = await this.hotelRepository.All().FirstOrDefaultAsync(h => h.Id == hotelId);

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

        public async Task EditAsync(int id, int hotelId, string name, string bulstat, string phoneNumber, string email, int cityId, string address)
        {
            var company = await this.companyRepository.All()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company.Bulstat != bulstat)
            {
                company = await this.companyRepository.All().FirstOrDefaultAsync(c => c.Bulstat == bulstat);

                var hotel = await this.hotelRepository.All().FirstOrDefaultAsync(h => h.Id == hotelId);

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

                return;
            }

            company.Name = name;
            company.PhoneNumber = phoneNumber;
            company.Email = email;
            company.CityId = cityId;
            company.Address = address;

            await this.companyRepository.SaveChangesAsync();
        }

        public async Task<T> GetById<T>(int id)
        {
            var company = await this.companyRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }
    }
}
