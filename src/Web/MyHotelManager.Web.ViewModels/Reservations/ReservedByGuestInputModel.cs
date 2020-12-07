namespace MyHotelManager.Web.ViewModels.Reservations
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservedByGuestInputModel : IMapTo<Guest>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
