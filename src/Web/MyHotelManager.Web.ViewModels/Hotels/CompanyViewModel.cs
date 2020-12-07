namespace MyHotelManager.Web.ViewModels.Hotels
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CompanyViewModel : IMapFrom<Company>
    {
        public string Address { get; set; }

        public string Bulstat { get; set; }

        public string CityName { get; set; }

        public string Email { get; set; }

        public string HotelsCount { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
