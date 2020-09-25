namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelCreateInputModel : IMapTo<Hotel>
    {
        [DisplayName("Hotel Name")]
        [MinLength(3)]
        [MaxLength(30)]
        [Required(ErrorMessage = "Name of the hotel is required!")]
        public string Name { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City of the hotel is required!")]
        public int CityId { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address of the hotel is required!")]
        public string Address { get; set; }

        [DisplayName("Hotel Stars")]
        [Required(ErrorMessage = "Stars of the hotel is required!")]
        public int StarsId { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company of the hotel is required!")]
        public int CompanyId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<StarsDropDownViewModel> Stars { get; set; }

        public IEnumerable<CompanyDropDownViewModel> Companies { get; set; }
    }
}
