namespace MyHotelManager.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AboutUsController : AdministrationController
    {
        public AboutUsController()
        {
        }

        public IActionResult ChangeAboutUs()
        {
            return this.View();
        }
    }
}
