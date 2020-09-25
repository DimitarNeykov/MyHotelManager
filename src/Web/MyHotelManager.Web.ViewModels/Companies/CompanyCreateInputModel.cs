namespace MyHotelManager.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CompanyCreateInputModel : IMapTo<Company>
    {
        [Required(ErrorMessage = "Name of the company is required!")]
        [MinLength(3)]
        [MaxLength(60)]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        // TODO..
        [Required(ErrorMessage = "Bulstat of the company is required!")]
        [DisplayName("Bulstat")]
        public string Bulstat { get; set; }

        [Required(ErrorMessage = "Phone number of the company is required!")]
        [RegularExpression(@"^\d{10}$")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email of the company is required!")]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address of the company is required!")]
        [MinLength(10)]
        [MaxLength(60)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City of the company is required!")]
        public int CityId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }
    }
}
