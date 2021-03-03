namespace MyHotelManager.Web.ViewModels.Payment
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PaymentInputModel
    {
        [DisplayName("Card Holder")]
        [MinLength(3)]
        [Required]
        public string Names { get; set; }

        public string Token { get; set; }

        public string PaymentId { get; set; }

        public string StripePublishableKey { get; set; }
    }
}
