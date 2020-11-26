namespace MyHotelManager.Web.ViewModels.Hotels
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelEditInputModel : IMapTo<Hotel>
    {
        public string Name { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public int StarsId { get; set; }

        public int CleaningPerDays { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }

        public IEnumerable<StarsDropDownViewModel> Stars { get; set; }
    }
}
