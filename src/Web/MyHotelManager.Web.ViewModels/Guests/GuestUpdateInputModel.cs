namespace MyHotelManager.Web.ViewModels.Guests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public class GuestUpdateInputModel : IMapTo<Guest>
    {
        public string Id { get; set; }

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
        [Range(1, 3, ErrorMessage = "Please choose gender from the drop down menu!")]
        [DisplayName("Gender")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Range(1, 257, ErrorMessage = "Please choose city from the drop down menu!")]
        [DisplayName("City")]
        public int? CityId { get; set; }

        [Range(1, 243, ErrorMessage = "Please choose country from the drop down menu!")]
        [DisplayName("Country")]
        public int? CountryId { get; set; }

        [ValidIdentificationNumber("Invalid identification number!")]
        [DisplayName("Identification Number")]
        public string IdentificationNumber { get; set; }

        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [DisplayName("Unique Number Foreigner")]
        public string UniqueNumberForeigner { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [RegularExpression("^([0-9]{9})$", ErrorMessage = "The field requires 9 digits!")]
        [DisplayName("Document Number")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DateAfterToday("Date of issue should not be before today!")]
        [DisplayName("Document Date Of Issue")]
        public DateTime? DateOfIssue { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DateBeforeToday("Date of expiry should not be after today!")]
        [DisplayName("Document Date Of Expiry")]
        public DateTime? DateOfExpiry { get; set; }

        public string ReservationId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<CountryDropDownViewModel> Countries { get; set; }

        public IEnumerable<GenderDropDownViewModel> Genders { get; set; }
    }
}
