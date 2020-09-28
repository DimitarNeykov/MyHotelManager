namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomCreateInputModel : IMapTo<Room>
    {
        public RoomCreateInputModel()
        {
            this.RoomTypes = new HashSet<RoomTypeDropDownViewModel>();
        }

        [Required]
        public string Number { get; set; }

        public string Description { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> RoomTypes { get; set; }
    }
}
