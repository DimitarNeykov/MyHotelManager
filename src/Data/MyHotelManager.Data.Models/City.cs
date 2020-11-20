using System.ComponentModel.DataAnnotations.Schema;

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

        public string Region { get; set; }

        public int Population { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Hotel> Hotels { get; set; }

        public ICollection<Guest> Guests { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
