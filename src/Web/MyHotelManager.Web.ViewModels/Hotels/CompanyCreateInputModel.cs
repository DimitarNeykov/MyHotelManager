namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public class CompanyCreateInputModel : IMapTo<Company>
    {
        [Required(ErrorMessage = "Полето е задължително!")]
        [MinLength(3, ErrorMessage = "Полето трябва да съдържа поне 3 символа!")]
        [MaxLength(60, ErrorMessage = "Максимално позволени символи: 60!")]
        [DisplayName("Име на фирмата")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [DisplayName("Булстат / ЕИК")]
        [ValidBulstat("Невалиден булстат!")]
        [MinLength(7)]
        [MaxLength(13)]
        public string Bulstat { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^\d{10}$")]
        [DisplayName("Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [EmailAddress(ErrorMessage = "Невалиден E-Mail адрес!")]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [MinLength(10, ErrorMessage = "Полето трябва да съдържа поне 10 символа!")]
        [MaxLength(60, ErrorMessage = "Максимално позволени символи: 60!")]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        [DisplayName("Град / Област")]
        [Required(ErrorMessage = "Полето е задължително!")]
        [Range(1, 257)]
        public int CityId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }
    }
}
