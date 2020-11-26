namespace MyHotelManager.Web.ViewModels.Hotels
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class HotelEditViewModel : IMapFrom<Hotel>
    {
        public string Name { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public int StarsId { get; set; }

        public int CleaningPerDays { get; set; }
    }
}
