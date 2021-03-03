namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomUpdateInputModel : IMapTo<Room>
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DisplayName("Room Number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "The adult count can not be less than 1!")]
        [DisplayName("Max Adult Count")]
        public int MaxAdultCount { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "The child count can not be negative!")]
        [DisplayName("Max Child Count")]
        public int MaxChildCount { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "The floor can not be negative!")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DisplayName("Room Type")]
        [Range(1, 8, ErrorMessage = "Please choose room type from the drop down menu!")]
        public int RoomTypeId { get; set; }

        [MaxLength(200, ErrorMessage = "The field must not be more than 200 characters!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(1, double.MaxValue, ErrorMessage = "The price can not be negative!")]
        public decimal Price { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> RoomTypes { get; set; }
    }
}
