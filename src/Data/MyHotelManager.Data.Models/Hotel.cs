﻿namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Hotel : BaseDeletableModel<int>
    {
        public Hotel()
        {
            this.Rooms = new HashSet<Room>();
            this.UsersHotels = new HashSet<UserHotel>();
        }

        public string Name { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string Address { get; set; }

        public int StarsId { get; set; }

        public virtual Stars Stars { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<UserHotel> UsersHotels { get; set; }
    }
}
