namespace MyHotelManager.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public class ReservationUpdateInputModel : IMapTo<Reservation>
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DateBeforeToday("Return date should not be after today!")]
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

        public int Nights => (int)(this.ReturnDate - this.ArrivalDate).TotalDays;

        [Required(ErrorMessage = "The field is required!")]
        [DisplayName("Adult Count")]
        public int? AdultCount { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [DisplayName("Child Count")]
        public int? ChildCount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The price can not be negative!")]
        [DisplayName("Custom Price")]
        public decimal CustomPrice { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "The price can not be negative!")]
        [DisplayName("All Price")]
        public decimal AllPrice { get; set; }

        [DisplayName("Breakfast")]
        public bool HasBreakfast { get; set; }

        [DisplayName("Lunch")]
        public bool HasLunch { get; set; }

        [DisplayName("Dinner")]
        public bool HasDinner { get; set; }

        [MaxLength(200, ErrorMessage = "The field must not be more than 200 characters!")]
        public string Description { get; set; }

        public ReservedByGuestInputModel ReservedByGuest { get; set; }

        public RoomViewModel Room { get; set; }
    }
}
