namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string GenderName { get; set; }
    }
}
