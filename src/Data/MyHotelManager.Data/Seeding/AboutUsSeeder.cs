namespace MyHotelManager.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public class AboutUsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.AboutUs.Any())
            {
                var aboutUs = new AboutUs
                {
                    Email = "dimitarneikov@gmail.com",
                    Phone = "0888989844",
                    Address = "Цариградски комплекс Дружба 2 284, 1582 ж.к. Дружба 2, София",
                    LocationUrlForGoogleMaps =
                        "https://www.google.com/maps/place/Цариградски+Комплекс+Бл.284/@@42.6469796,23.4019008,15z/data=!4m5!3m4!1s0x0:0xf39840e6ad788839!8m2!3d42.6469796!4d23.4019008",
                    LocationUrlForOpenStreetMap =
                        "https://openstreetmap.org/export/embed.html?bbox=23.40328%2C42.64617%2C23.39128%2C42.64717&amp;layer=mapnik&amp;marker=42.64717%2C23.40128",
                };

                var result = await dbContext.AboutUs.AddAsync(aboutUs);
            }
        }
    }
}
