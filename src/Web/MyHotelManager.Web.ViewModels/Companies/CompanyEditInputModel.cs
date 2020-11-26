namespace MyHotelManager.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class CompanyEditInputModel : IMapTo<Company>
    {
        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityDropDownViewModel> Cities { get; set; }
    }
}
