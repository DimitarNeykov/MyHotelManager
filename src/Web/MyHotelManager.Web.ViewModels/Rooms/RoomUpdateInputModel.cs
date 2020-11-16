using System.Collections.Generic;

namespace MyHotelManager.Web.ViewModels.Rooms
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomUpdateInputModel : IMapTo<Room>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int MaxAdultCount { get; set; }

        public int MaxChildCount { get; set; }

        public int Floor { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> RoomTypes { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
