namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Cleaning;

    public class CleaningController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CleaningController(IRoomsService roomsService, UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> DailyList()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.roomsService.GetAllRoomsForCleaningToday<CleaningRoomViewModel>((int) user.HotelId);

            return this.View(viewModel);
        }
    }
}
