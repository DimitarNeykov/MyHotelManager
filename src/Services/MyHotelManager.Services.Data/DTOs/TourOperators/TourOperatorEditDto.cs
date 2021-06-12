namespace MyHotelManager.Services.Data.DTOs.TourOperators
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorEditDto : IMapTo<TourOperator>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TourOperatorAgentEditDto Agent { get; set; }

        public TourOperatorCompanyEditDto Company { get; set; }
    }
}
