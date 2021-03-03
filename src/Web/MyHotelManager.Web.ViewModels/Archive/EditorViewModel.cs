namespace MyHotelManager.Web.ViewModels.Archive
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class EditorViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
