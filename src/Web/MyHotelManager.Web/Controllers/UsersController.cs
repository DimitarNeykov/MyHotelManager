namespace MyHotelManager.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Services.Messaging;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Users;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGendersService gendersService;
        private readonly IMailHelper mailHelper;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IGendersService gendersService,
            IMailHelper mailHelper)
        {
            this.userManager = userManager;
            this.gendersService = gendersService;
            this.mailHelper = mailHelper;
        }

        public IActionResult Create()
        {
            var genders = this.gendersService.GetAll<GenderDropDownViewModel>();

            var viewModel = new UserCreateInputModel
            {
                Genders = genders,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var genders = this.gendersService.GetAll<GenderDropDownViewModel>();
                input.Genders = genders;

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var newUser = new ApplicationUser
            {
                BirthDate = input.BirthDate,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber,
                GenderId = input.GenderId,
                UserName = input.Username,
                Email = input.Email,
                HotelId = user.HotelId,
            };
            var result = await this.userManager.CreateAsync(newUser, input.Password);
            if (result.Succeeded)
            {
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(newUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = newUser.Id, code = code },
                    protocol: this.Request.Scheme);

                await this.mailHelper.SendFromIdentityAsync(
                    input.Email,
                    "Confirm your registration",
                    $"{newUser.FirstName} {newUser.LastName}",
                    "You are receiving this email because we received a registration confirmation request.",
                    HtmlEncoder.Default.Encode(callbackUrl),
                    "If you did not request a registration confirmation, no further action is needed.");
            }

            switch (input.Role)
            {
                case GlobalConstants.ManagerRoleName:
                    await this.userManager.AddToRoleAsync(newUser, GlobalConstants.ManagerRoleName);
                    break;
                case GlobalConstants.ReceptionistRoleName:
                    await this.userManager.AddToRoleAsync(newUser, GlobalConstants.ReceptionistRoleName);
                    break;
            }

            return this.RedirectToAction("Manager", "Hotels");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, string role)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (!await this.CheckIsValidHotelAndUser(user.Id, (int)user.HotelId))
            {
                return this.NotFound();
            }

            var userRoles = await this.userManager.GetRolesAsync(user);

            switch (role)
            {
                case GlobalConstants.ManagerRoleName:
                    await this.userManager.RemoveFromRolesAsync(user, userRoles);
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);
                    break;
                case GlobalConstants.ReceptionistRoleName:
                    await this.userManager.RemoveFromRolesAsync(user, userRoles);
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.ReceptionistRoleName);
                    break;
            }

            return this.RedirectToAction("Manager", "Hotels");
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (!await this.CheckIsValidHotelAndUser(user.Id, (int)user.HotelId))
            {
                return this.NotFound();
            }

            user.IsDeleted = true;
            await this.userManager.UpdateAsync(user);
            return this.RedirectToAction("Manager", "Hotels");
        }

        private async Task<bool> CheckIsValidHotelAndUser(string userId, int hotelId)
        {
            var manager = await this.userManager.GetUserAsync(this.User);

            var hotelCreator = await this.userManager.Users.Where(u => u.HotelId == manager.HotelId).OrderBy(u => u.CreatedOn).FirstOrDefaultAsync();

            if (hotelId != manager.HotelId || userId == manager.Id || userId == hotelCreator.Id)
            {
                return false;
            }

            return true;
        }
    }
}
