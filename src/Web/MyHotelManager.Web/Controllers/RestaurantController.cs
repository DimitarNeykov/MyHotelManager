namespace MyHotelManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RestaurantController : Controller
    {
        public RestaurantController()
        {
        }

        public IActionResult Breakfast()
        {
            return this.View();
        }

        public IActionResult Lunch()
        {
            return this.View();
        }

        public IActionResult Dinner()
        {
            return this.View();
        }
    }
}
