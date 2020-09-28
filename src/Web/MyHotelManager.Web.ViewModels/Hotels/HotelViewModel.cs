namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelViewModel : IMapFrom<Hotel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public int StarsId { get; set; }

        public int CompanyId { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<UserHotel> UsersHotels { get; set; }
    }
}
