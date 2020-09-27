namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Gender : BaseDeletableModel<int>
    {
        public Gender()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Guests = new HashSet<Guest>();
        }

        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
