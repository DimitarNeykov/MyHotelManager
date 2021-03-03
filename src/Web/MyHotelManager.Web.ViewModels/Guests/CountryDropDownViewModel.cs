namespace MyHotelManager.Web.ViewModels.Guests
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CountryDropDownViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
