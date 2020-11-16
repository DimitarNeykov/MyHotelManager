namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DatesInputViewModel
    {
        [Required]
        public DateTime? ArrivalDate { get; set; }

        [Required]
        public DateTime? ReturnDate { get; set; }
    }
}
