namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var countries = this.countriesRepository.All().OrderBy(x => x.Name).To<T>().ToList();

            return countries;
        }
    }
}
