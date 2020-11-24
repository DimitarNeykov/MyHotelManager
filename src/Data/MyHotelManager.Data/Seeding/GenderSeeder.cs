namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public class GenderSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Genders.Any())
            {
                var genders = new List<string>
                {
                    "Male",
                    "Female",
                    "Other",
                };

                foreach (var gender in genders)
                {
                    await dbContext.Genders.AddAsync(new Gender
                    {
                        Name = gender,
                    });
                }
            }
        }
    }
}
