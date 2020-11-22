namespace MyHotelManager.Web.ViewModels.Guests
{
    using System;
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GuestUpdateInputModel : IMapTo<Guest>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public string PhoneNumber { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public string UCN { get; set; }

        public string PNF { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        public string ReservationId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<CountryDropDownViewModel> Countries { get; set; }
    }
}
