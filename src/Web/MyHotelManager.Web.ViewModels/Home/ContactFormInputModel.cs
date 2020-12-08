namespace MyHotelManager.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ContactFormInputModel : IMapTo<ContactForm>
    {
        [Required(ErrorMessage = "The field is required!")]
        [MinLength(5, ErrorMessage = "The field requires more than 5 characters!")]
        [MaxLength(40, ErrorMessage = "The field must not be more than 40 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
        [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(5, ErrorMessage = "The field requires more than 5 characters!")]
        [MaxLength(100, ErrorMessage = "The field must not be more than 100 characters!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(20, ErrorMessage = "The field requires more than 20 characters!")]
        [MaxLength(10000, ErrorMessage = "The field must not be more than 10000 characters!")]
        public string Content { get; set; }

        public AboutUsViewModel AboutUs { get; set; }
    }
}
