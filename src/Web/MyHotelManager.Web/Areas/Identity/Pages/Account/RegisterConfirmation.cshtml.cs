namespace MyHotelManager.Web.Areas.Identity.Pages.Account
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Messaging;

    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailHelper mailHelper;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IMailHelper mailHelper)
        {
            this._userManager = userManager;
            this.mailHelper = mailHelper;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return this.RedirectToPage("/Index");
            }

            var user = await this._userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with email '{email}'.");
            }

            this.Email = email;

            this.TempData["MessageType"] = "Success";
            this.TempData["Message"] =
                "Thanks for signing up. Please visit your email and verify your account to continue.";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
