namespace MyHotelManager.Web.ViewModels.Companies
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CityDropDownViewModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
