namespace MyHotelManager.Web.ViewModels.Guests
{
    using System;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GuestUpdateViewModel : IMapFrom<Guest>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public string PhoneNumber { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public string IdentificationNumber { get; set; }

        public string UniqueNumberForeigner { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        public string ReservationId { get; set; }
    }
}
