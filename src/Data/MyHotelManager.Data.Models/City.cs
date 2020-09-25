namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Hotels = new HashSet<Hotel>();
            this.Guests = new HashSet<Guest>();
            this.Companies = new HashSet<Company>();
        }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
