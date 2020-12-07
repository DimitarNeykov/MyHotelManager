namespace MyHotelManager.Web.ViewModels.Archive
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservedByGuestViewModel : IMapFrom<Guest>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
