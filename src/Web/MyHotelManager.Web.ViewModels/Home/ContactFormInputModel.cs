namespace MyHotelManager.Web.ViewModels.Home
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ContactFormInputModel : IMapTo<ContactForm>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
