namespace MyHotelManager.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Messaging;
    using MyHotelManager.Web.Infrastructure.Attributes;

    [AllowAnonymous]
    public partial class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMailHelper mailHelper;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IMailHelper mailHelper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this.mailHelper = mailHelper;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "The field is required!")]
            [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
            [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
            [DisplayName("First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
            [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
            [DisplayName("Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [DateAfterToday("Birth date should not be before today!")]
            [DisplayName("Birth Date")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [Range(1, 3, ErrorMessage = "Please choose sex from the drop down menu!")]
            [DisplayName("Gender")]
            public int GenderId { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
            [DisplayName("Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
            [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
            public string Username { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [EmailAddress(ErrorMessage = "Invalid Email!")]
            [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
            [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [RegularExpression(@"^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[@$!% *? &])[A - Za - z\d@$!% *? &]{8,}$", ErrorMessage = "The field requires 10 digits!")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    BirthDate = this.Input.BirthDate,
                    GenderId = this.Input.GenderId,
                    PhoneNumber = this.Input.PhoneNumber,
                    UserName = this.Input.Username,
                    Email = this.Input.Email,
                };
                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.mailHelper.SendFromIdentityAsync(
                        this.Input.Email,
                        "Confirm your registration",
                        $"{user.FirstName} {user.LastName}",
                        "You are receiving this email because we received a registration confirmation request.",
                        HtmlEncoder.Default.Encode(callbackUrl),
                        "If you did not request a registration confirmation, no further action is needed.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
