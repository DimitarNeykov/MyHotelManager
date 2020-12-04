namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class AboutUs : BaseDeletableModel<int>
    {
        public string Email { get; set; }

        public string EmailPassword { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string LocationUrlForGoogleMaps { get; set; }

        public string LocationUrlForOpenStreetMap { get; set; }
    }
}
