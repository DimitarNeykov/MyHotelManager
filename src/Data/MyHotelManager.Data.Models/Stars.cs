namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Stars : BaseDeletableModel<int>
    {
        public Stars()
        {
            this.Hotels = new HashSet<Hotel>();
        }

        public string Name { get; set; }

        public int StarsInNumbers { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }
}
