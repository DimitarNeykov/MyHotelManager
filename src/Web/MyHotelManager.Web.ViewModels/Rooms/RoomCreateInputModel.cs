namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomCreateInputModel : IMapTo<Room>
    {
        [Required]
        public string Number { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int MaxAdultCount { get; set; }

        [Required]
        public int MaxChildCount { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        public int Floor { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> RoomTypes { get; set; }
    }
}
