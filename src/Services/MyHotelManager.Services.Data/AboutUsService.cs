namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class AboutUsService : IAboutUsService
    {
        private readonly IDeletableEntityRepository<AboutUs> aboutUsRepository;

        public AboutUsService(IDeletableEntityRepository<AboutUs> aboutUsRepository)
        {
            this.aboutUsRepository = aboutUsRepository;
        }

        public async Task<T> GetInformationAsync<T>()
        {
            var aboutUsInformation = await this.aboutUsRepository
                .All()
                .To<T>()
                .FirstOrDefaultAsync();

            return aboutUsInformation;
        }
    }
}
