namespace MyHotelManager.Services.Data.DTOs.TourOperators
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorAgentEditDto : IMapTo<TourOperatorAgent>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
