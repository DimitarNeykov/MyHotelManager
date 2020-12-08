namespace MyHotelManager.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public class CompanyEditInputModel : IMapTo<Company>
    {
        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(40, ErrorMessage = "The field must not be more than 40 characters!")]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [ValidBulstat("Invalid Bulstat!")]
        public string Bulstat { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
        [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
        [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(1, 257, ErrorMessage = "Please choose city from the drop down menu!")]
        [DisplayName("City")]
        public int CityId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }
    }
}
