using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelEditInputModel : IMapTo<Hotel>
    {
        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(40, ErrorMessage = "The field must not be more than 40 characters!")]
        [DisplayName("Hotel Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(1, 257, ErrorMessage = "Please choose city from the drop down menu!")]
        [DisplayName("City")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
        [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
        public string Address { get; set; }

        [DisplayName("Stars")]
        [Required(ErrorMessage = "The field is required!")]
        [Range(1, 6, ErrorMessage = "Please choose stars from the drop down menu!")]
        public int StarsId { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(1, 30, ErrorMessage = "Please input cleaning per day!")]
        [DisplayName("Cleaning Per Day")]
        public int CleaningPerDays { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<StarsDropDownViewModel> Stars { get; set; }
    }
}
