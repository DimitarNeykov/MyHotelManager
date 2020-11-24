namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Seeding.ImportDto;
    using Newtonsoft.Json;

    public class CountriesSeeder : ISeeder
    {
        private readonly string contentRootPath;

        public CountriesSeeder(string contentRootPath)
        {
            this.contentRootPath = contentRootPath;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Cities.Any())
            {
                var json = JsonConvert.DeserializeObject<List<CountriesImportDto>>(await File.ReadAllTextAsync($"{this.contentRootPath}/JsonInput/countries.json"));

                var countries = new List<Country>();

                foreach (var country in json)
                {
                    var validCountry = new Country
                    {
                        Name = country.Name,
                        Code = country.Code,
                    };

                    countries.Add(validCountry);
                }

                await dbContext.Countries.AddRangeAsync(countries);
            }
        }
    }
}
