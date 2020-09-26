namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using MyHotelManager.Data.Models;

    public class StarsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Stars.Any())
            {
                var stars = new List<(string Name, int StarsInNumbers)>
                {
                    ("One", 1),
                    ("Two", 2),
                    ("Three", 3),
                    ("Four", 4),
                    ("Five", 5),
                    ("Michelin", 6),
                };

                foreach (var star in stars)
                {
                    await dbContext.Stars.AddAsync(new Stars
                    {
                        Name = star.Name,
                        StarsInNumbers = star.StarsInNumbers,
                    });
                }
            }
        }
    }
}
