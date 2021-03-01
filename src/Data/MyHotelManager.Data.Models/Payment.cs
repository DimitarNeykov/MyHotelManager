namespace MyHotelManager.Data.Models
{
    using System;

    using MyHotelManager.Data.Common.Models;

    public class Payment : BaseModel<string>
    {
        public string Status { get; set; }

        public decimal Price { get; set; }

        public int PackagePeriod { get; set; }

        public string PaymentId { get; set; }

        public string CardHolder { get; set; }

        public string Ip { get; set; }

        public string LastFourCardDigit { get; set; }

        public DateTime CardExpires { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
