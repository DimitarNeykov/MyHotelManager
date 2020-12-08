namespace MyHotelManager.Web.ViewModels.Users
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GenderDropDownViewModel : IMapFrom<Gender>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
