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
                var stars = new List<string>
                {
                    "One",
                    "Two",
                    "Three",
                    "Four",
                    "Five",
                    "Michelin",
                };

                foreach (var star in stars)
                {
                    await dbContext.Stars.AddAsync(new Stars
                    {
                        Name = star,
                    });
                }
            }
        }
    }
}
