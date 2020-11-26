namespace MyHotelManager.Web.ViewModels.Companies
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

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }
    }
}
