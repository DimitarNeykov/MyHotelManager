namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using MyHotelManager.Data.Models;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Cities.Any())
            {
                var cities = new List<(string, string)>
                {
                    ("Sofia", "BG"),
                    ("Stara Zagora", "BG"),
                    ("Burgas", "BG"),
                    ("Varna", "BG"),
                    ("Plovdiv", "BG"),
                };

                foreach (var city in cities)
                {
                    await dbContext.Cities.AddAsync(new City
                    {
                        Name = city.Item1,
                        CountryCode = city.Item2,
                    });
                }
            }
        }
    }
}
