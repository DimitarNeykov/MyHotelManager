namespace MyHotelManager.Web.ViewModels.Companies
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CompanyEditViewModel : IMapFrom<Company>
    {
        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }
    }
}
