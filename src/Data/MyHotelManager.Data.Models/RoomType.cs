namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class RoomType : BaseDeletableModel<int>
    {
        public RoomType()
        {
            this.Rooms = new HashSet<Room>();
        }

        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
