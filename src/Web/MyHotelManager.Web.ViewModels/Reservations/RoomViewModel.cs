namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomViewModel : IMapFrom<Room>
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Room Number")]
        public string Number { get; set; }

        [DisplayName("Max Adult Count")]
        public int MaxAdultCount { get; set; }

        [DisplayName("Max Child Count")]
        public int MaxChildCount { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Room Type")]
        public string RoomTypeName { get; set; }

        public string Description { get; set; }

        public string Floor { get; set; }

        public int HotelId { get; set; }
    }
}
