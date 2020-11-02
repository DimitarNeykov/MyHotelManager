namespace MyHotelManager.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId != null)
            {
                return this.RedirectToAction("Index", "Hotels");
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
