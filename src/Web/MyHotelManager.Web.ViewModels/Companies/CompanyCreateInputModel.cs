namespace MyHotelManager.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Name { get; set; }

        // TODO..
        [Required]
        public string Bulstat { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(60)]
        public string Address { get; set; }
    }
}
