using System.Threading.Tasks;
using MyHotelManager.Services.Data;
using MyHotelManager.Web.ViewModels.Home;

namespace MyHotelManager.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IAboutUsService aboutUsService;

        public HomeController(IAboutUsService aboutUsService)
        {
            this.aboutUsService = aboutUsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> ContactUs()
        {
            var aboutUsInformation = await this.aboutUsService.GetInformationAsync<AboutUsViewModel>();

            var viewModel = new ContactFormInputModel
            {
                AboutUs = aboutUsInformation,
            };

            return this.View(viewModel);
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
