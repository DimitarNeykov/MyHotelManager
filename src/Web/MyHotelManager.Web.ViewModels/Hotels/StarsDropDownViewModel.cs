namespace MyHotelManager.Web.ViewModels.Hotels
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class StarsDropDownViewModel : IMapFrom<Stars>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
