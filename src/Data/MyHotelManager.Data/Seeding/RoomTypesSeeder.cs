namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using MyHotelManager.Data.Models;

    public class RoomTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.RoomTypes.Any())
            {
                var types = new List<string>
                {
                    "Single",
                    "Double",
                    "Triple",
                    "Quad",
                    "Studio",
                    "Apartment",
                    "Presidential",
                    "Disabled",
                };

                foreach (var type in types)
                {
                    await dbContext.RoomTypes.AddAsync(new RoomType
                    {
                        Name = type,
                    });
                }
            }
        }
    }
}
