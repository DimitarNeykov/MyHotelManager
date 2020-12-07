namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;

    using AutoMapper;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GuestViewModel : IMapFrom<Guest>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GenderName { get; set; }

        public string PhoneNumber { get; set; }

        public string CityName { get; set; }

        public string IdentificationNumber { get; set; }

        public string UniqueNumberForeigner { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        public string Country { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guest, GuestViewModel>()
                .ForMember(x => x.Country, opt =>
                    opt.MapFrom(x => x.Country.Name ?? x.City.Country.Name));
        }
    }
}
