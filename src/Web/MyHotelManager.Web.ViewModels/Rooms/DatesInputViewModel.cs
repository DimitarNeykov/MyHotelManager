namespace MyHotelManager.Web.ViewModels.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DatesInputViewModel
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}
