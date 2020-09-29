namespace MyHotelManager.Web.ViewModels.Reservations
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomsDropDownViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public string Number { get; set; }
    }
}
