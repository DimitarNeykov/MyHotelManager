namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelViewModel : IMapFrom<Hotel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CityName { get; set; }

        public string Address { get; set; }

        public int StarsStarsInNumbers { get; set; }

        public string CompanyName { get; set; }

        public Company Company { get; set; }

        public int CleaningPerDays { get; set; }

        public int RoomsCount { get; set; }

        public int UsersCount { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public string Role { get; set; }
    }
}
