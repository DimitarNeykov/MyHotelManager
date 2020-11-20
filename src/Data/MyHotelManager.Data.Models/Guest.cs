namespace MyHotelManager.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Guest : BaseDeletableModel<string>
    {
        public Guest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public string UCN { get; set; }

        public string PNF { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
