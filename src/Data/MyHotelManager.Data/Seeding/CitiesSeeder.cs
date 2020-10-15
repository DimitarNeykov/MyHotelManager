namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using MyHotelManager.Data.Models;
    using Newtonsoft.Json;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Cities.Any())
            {
                var json = JsonConvert.DeserializeObject<List<CitiesImportDto>>(await File.ReadAllTextAsync(@"wwwroot\JsonInput\towns.json"));

                var cities = new List<City>();

                foreach (var city in json)
                {
                    var validCity = new City
                    {
                        Name = city.Name,
                        Region = city.Region,
                        Population = city.Population,
                        CountryCode = "BG",
                    };

                    cities.Add(validCity);
                }

                await dbContext.Cities.AddRangeAsync(cities);
            }
        }
    }
}
