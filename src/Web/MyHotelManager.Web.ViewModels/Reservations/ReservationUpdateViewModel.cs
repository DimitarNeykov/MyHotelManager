namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using AutoMapper;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationUpdateViewModel : IMapFrom<Reservation>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public RoomViewModel Room { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Nights => (int)(this.ReturnDate - this.ArrivalDate).TotalDays;

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Breakfast")]
        public bool HasBreakfast { get; set; }

        [DisplayName("Lunch")]
        public bool HasLunch { get; set; }

        [DisplayName("Dinner")]
        public bool HasDinner { get; set; }

        public string Description { get; set; }

        public ReservedByGuestViewModel ReservedByGuest { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reservation, ReservationUpdateViewModel>()
                .ForMember(x => x.ReservedByGuest, opt =>
                    opt.MapFrom(x => x.Guests.OrderBy(g => g.CreatedOn).FirstOrDefault()));
        }
    }
}
