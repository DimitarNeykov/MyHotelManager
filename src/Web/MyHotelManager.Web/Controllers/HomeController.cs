namespace MyHotelManager.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Services.Messaging;
    using MyHotelManager.Web.ViewModels;
    using MyHotelManager.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IAboutUsService aboutUsService;
        private readonly IContactUsService contactUsService;
        private readonly IMailHelper mailHelper;

        public HomeController(
            IAboutUsService aboutUsService,
            IContactUsService contactUsService,
            IMailHelper mailHelper)
        {
            this.aboutUsService = aboutUsService;
            this.contactUsService = contactUsService;
            this.mailHelper = mailHelper;
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

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.contactUsService.CreateAsync(input.Name, input.Email, input.Title, input.Content);

            this.mailHelper.SendContactForm(input.Email, input.Name, input.Title, input.Content);

            return this.RedirectToAction("Index");
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
