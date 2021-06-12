namespace MyHotelManager.Web.ViewModels.TourOperators
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorEditInputModel : IMapFrom<TourOperator>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
        public string Name { get; set; }

        public TourOperatorAgentEditInputModel Agent { get; set; }

        public TourOperatorCompanyEditInputModel Company { get; set; }
    }
}
