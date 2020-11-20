namespace MyHotelManager.Web.ViewModels.Guests
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CityDropDownViewModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }
    }
}
