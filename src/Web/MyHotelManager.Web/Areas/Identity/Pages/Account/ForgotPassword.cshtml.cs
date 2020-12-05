namespace MyHotelManager.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Messaging;

    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailHelper mailHelper;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IMailHelper mailHelper)
        {
            this._userManager = userManager;
            this.mailHelper = mailHelper;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(this.Input.Email);
                if (user == null || !(await this._userManager.IsEmailConfirmedAsync(user)))
                {
                    this.TempData["Message"] = "Error! We could not find a user with this E-mail.";
                    return this.RedirectToAction("Index", "Home");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await this._userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: this.Request.Scheme);

                await this.mailHelper.SendFromIdentityAsync(
                    this.Input.Email,
                    "Password reset",
                    $"{user.FirstName} {user.LastName}",
                    "You are receiving this email because we received a password reset request for your account.",
                    HtmlEncoder.Default.Encode(callbackUrl),
                    "If you did not request a password reset, no further action is required.");

                return this.RedirectToPage("./ForgotPasswordConfirmation");
            }

            return this.Page();
        }
    }
}
