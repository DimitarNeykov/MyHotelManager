namespace MyHotelManager.Services.Data.DTOs.TourOperators
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorCompanyCreateDto : IMapTo<TourOperatorCompany>
    {
        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
