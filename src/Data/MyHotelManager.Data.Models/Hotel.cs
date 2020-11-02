namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Hotel : BaseDeletableModel<int>
    {
        public Hotel()
        {
            this.Rooms = new HashSet<Room>();
            this.Users = new HashSet<ApplicationUser>();
            this.Guests = new HashSet<Guest>();
        }

        public string Name { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string Address { get; set; }

        public int StarsId { get; set; }

        public Stars Stars { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
