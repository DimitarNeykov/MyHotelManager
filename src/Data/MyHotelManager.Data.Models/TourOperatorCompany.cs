namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class TourOperatorCompany : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
