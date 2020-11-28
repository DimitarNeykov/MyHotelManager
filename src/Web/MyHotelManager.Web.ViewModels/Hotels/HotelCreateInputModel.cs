﻿namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.ViewModels.Companies;

    public class HotelCreateInputModel : IMapTo<Hotel>
    {
        [DisplayName("Име на хотела")]
        [MinLength(3, ErrorMessage = "Полето трябва да съдържа поне 3 символа!")]
        [MaxLength(30, ErrorMessage = "Максимално позволени символи: 30!")]
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Name { get; set; }

        [DisplayName("Град / Област")]
        [Required(ErrorMessage = "Полето е задължително!")]
        [Range(1, 257)]
        public int CityId { get; set; }

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Address { get; set; }

        [DisplayName("Звезди")]
        [Required(ErrorMessage = "Полето е задължително!")]
        [Range(1, 6)]
        public int StarsId { get; set; }

        public int CleaningPerDays { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<StarsDropDownViewModel> Stars { get; set; }
    }
}
