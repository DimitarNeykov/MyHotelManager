namespace MyHotelManager.Web.ViewModels.Rooms
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomTypeDropDownViewModel : IMapFrom<RoomType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
