namespace MyHotelManager.Web.ViewModels.Cleaning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CleaningRoomViewModel : IMapFrom<Room>, IHaveCustomMappings
    {
        public string Number { get; set; }

        public string RoomTypeName { get; set; }

        public DateTime ReservationReturnDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, CleaningRoomViewModel>()
                .ForMember(x => x.ReservationReturnDate, opt =>
                    opt.MapFrom(x => x.Reservations.FirstOrDefault(r =>
                        (DateTime.UtcNow.Date - r.ArrivalDate.Date).Days % x.Hotel.CleaningPerDays == 0 && r.ArrivalDate.Date < DateTime.UtcNow.Date ||
                        r.ReturnDate.Date == DateTime.UtcNow.Date).ReturnDate));
        }
    }
}
