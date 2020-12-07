namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string RoomNumber { get; set; }

        public CreatorViewModel Creator { get; set; }

        public DateTime BookDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Description { get; set; }

        public ReservedByGuestViewModel ReservedByGuest { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reservation, ReservationViewModel>()
                .ForMember(x => x.ReservedByGuest, opt =>
                    opt.MapFrom(x => x.Guests.OrderBy(g => g.CreatedOn).First()));
        }
    }
}
