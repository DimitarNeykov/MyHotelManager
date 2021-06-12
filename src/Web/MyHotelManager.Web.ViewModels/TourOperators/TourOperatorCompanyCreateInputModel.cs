namespace MyHotelManager.Web.ViewModels.TourOperators
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public class TourOperatorCompanyCreateInputModel : IMapTo<TourOperatorCompany>
    {
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
        public string Name { get; set; }

        [ValidBulstat("Invalid Bulstat!")]
        public string Bulstat { get; set; }

        [EmailAddress]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        public string PhoneNumber { get; set; }
    }
}
