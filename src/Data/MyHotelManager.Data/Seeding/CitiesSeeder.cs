namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Seeding.ImportDto;
    using Newtonsoft.Json;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Cities.Any())
            {
                var json = JsonConvert.DeserializeObject<List<CitiesImportDto>>(await File.ReadAllTextAsync(@"wwwroot\JsonInput\towns.json"));

                var cities = new List<City>();

                var bgCountry = await dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Bulgaria");

                foreach (var city in json)
                {
                    var validCity = new City
                    {
                        Name = city.Name,
                        Region = city.Region,
                        Population = city.Population,
                        CountryId = bgCountry.Id,
                    };

                    cities.Add(validCity);
                }

                await dbContext.Cities.AddRangeAsync(cities);
            }
        }
    }
}
