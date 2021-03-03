namespace MyHotelManager.Web.ViewModels.Guests
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GenderDropDownViewModel : IMapFrom<Gender>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
