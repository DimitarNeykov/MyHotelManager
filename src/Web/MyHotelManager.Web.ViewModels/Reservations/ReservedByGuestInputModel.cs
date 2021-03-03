namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservedByGuestInputModel : IMapTo<Guest>
    {
        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(20, ErrorMessage = "The field must not be more than 20 characters!")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(20, ErrorMessage = "The field must not be more than 20 characters!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
