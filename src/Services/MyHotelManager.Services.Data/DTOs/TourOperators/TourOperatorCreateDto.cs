namespace MyHotelManager.Services.Data.DTOs.TourOperators
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorCreateDto : IMapTo<TourOperator>
    {
        public string Name { get; set; }

        public TourOperatorAgentCreateDto Agent { get; set; }

        public TourOperatorCompanyCreateDto Company { get; set; }

        public int HotelId { get; set; }
    }
}
