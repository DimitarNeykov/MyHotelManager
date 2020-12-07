namespace MyHotelManager.Web.ViewModels.Restaurant
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>, IHaveCustomMappings
    {
        public string RoomNumber { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public int TotalCount => this.AdultCount + this.ChildCount;

        public IEnumerable<GuestViewModel> Guests { get; set; }

        public string Country { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reservation, ReservationViewModel>()
                .ForMember(x => x.Country, opt =>
                    opt.MapFrom(x => x.Guests.OrderBy(g => g.CreatedOn).FirstOrDefault().Country.Name != null ?
                        x.Guests.OrderBy(g => g.CreatedOn).FirstOrDefault().Country.Name :
                        x.Guests.OrderBy(g => g.CreatedOn).FirstOrDefault().City.Country.Name));
        }
    }
}
