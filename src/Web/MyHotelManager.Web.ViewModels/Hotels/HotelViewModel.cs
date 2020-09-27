namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelViewModel : IMapFrom<Hotel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserHotel> UsersHotels { get; set; }
    }
}
