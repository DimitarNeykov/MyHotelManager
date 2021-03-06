﻿namespace MyHotelManager.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Web.Infrastructure.Attributes;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

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
            [DateBeforeToday("Birth date should not be after today!")]
            [DisplayName("Birth Date")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
            [DisplayName("Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "The field is required!")]
            [Range(1, 3, ErrorMessage = "Please choose sex from the drop down menu!")]
            [DisplayName("Gender")]
            public int GenderId { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            this.Username = userName;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                GenderId = (int)user.GenderId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
            };
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

            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";

                    return this.RedirectToPage();
                }
            }

            user.FirstName = this.Input.FirstName;
            user.LastName = this.Input.LastName;
            user.BirthDate = this.Input.BirthDate;
            user.GenderId = this.Input.GenderId;

            await this._userManager.UpdateAsync(user);

            await this._signInManager.RefreshSignInAsync(user);

            this.StatusMessage = "Your profile has been updated";

            return this.RedirectToPage();
        }
    }
}
