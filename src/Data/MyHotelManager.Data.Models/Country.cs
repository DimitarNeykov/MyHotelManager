namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Guests = new HashSet<Guest>();
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
