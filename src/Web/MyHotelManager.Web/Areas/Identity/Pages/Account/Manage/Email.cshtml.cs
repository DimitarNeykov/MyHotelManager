namespace MyHotelManager.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Messaging;

    public partial class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailHelper mailHelper;

        public EmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMailHelper mailHelper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.mailHelper = mailHelper;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await this._userManager.GetEmailAsync(user);
            this.Email = email;

            this.Input = new InputModel
            {
                NewEmail = email,
            };

            this.IsEmailConfirmed = await this._userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var email = await this._userManager.GetEmailAsync(user);
            if (this.Input.NewEmail != email)
            {
                var userId = await this._userManager.GetUserIdAsync(user);
                var code = await this._userManager.GenerateChangeEmailTokenAsync(user, this.Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = this.Input.NewEmail, code = code },
                    protocol: this.Request.Scheme);

                string sendToAddress;

                if (await this._userManager.IsEmailConfirmedAsync(user))
                {
                    sendToAddress = email;
                }
                else
                {
                    sendToAddress = this.Input.NewEmail;
                }

                await this.mailHelper.SendFromIdentityAsync(
                    sendToAddress,
                    "Confirm your email",
                    $"{user.FirstName} {user.LastName}",
                    "You are receiving this email because we received an email reset request for your account in My Hotel Manager.",
                    HtmlEncoder.Default.Encode(callbackUrl),
                    "If you did not request an email reset, no further action is required.");

                this.StatusMessage = "Confirmation link to change email sent. Please check your email.";

                return this.RedirectToPage();
            }

            this.StatusMessage = "Your email is unchanged.";
            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var userId = await this._userManager.GetUserIdAsync(user);

            var email = await this._userManager.GetEmailAsync(user);

            var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: this.Request.Scheme);

            this.StatusMessage = "Verification email sent. Please check your email.";
            return this.RedirectToPage();
        }
    }
}
