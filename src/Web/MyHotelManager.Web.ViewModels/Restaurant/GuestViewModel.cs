namespace MyHotelManager.Web.ViewModels.Restaurant
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GuestViewModel : IMapFrom<Guest>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
