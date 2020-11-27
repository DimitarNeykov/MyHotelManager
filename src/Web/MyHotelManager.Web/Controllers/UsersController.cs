namespace MyHotelManager.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
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

            await this.userManager.CreateAsync(newUser, input.Password);

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

            var manager = await this.userManager.GetUserAsync(this.User);

            var hotelCreator = await this.userManager.Users.Where(u => u.HotelId == manager.HotelId).OrderBy(u => u.CreatedOn).FirstOrDefaultAsync();

            if (user.HotelId != manager.HotelId || user.Id == manager.Id || user.Id == hotelCreator.Id)
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

            var manager = await this.userManager.GetUserAsync(this.User);

            var hotelCreator = await this.userManager.Users.Where(u => u.HotelId == manager.HotelId).OrderBy(u => u.CreatedOn).FirstOrDefaultAsync();

            if (user.HotelId != manager.HotelId || user.Id == manager.Id || user.Id == hotelCreator.Id)
            {
                return this.NotFound();
            }

            user.IsDeleted = true;

            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction("Manager", "Hotels");
        }
    }
}
