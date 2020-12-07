namespace MyHotelManager.Web.ViewModels.Reservations
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CreatorViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
