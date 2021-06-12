namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class TourOperatorAgent : BaseDeletableModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
