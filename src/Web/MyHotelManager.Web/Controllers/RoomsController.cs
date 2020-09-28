namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Rooms;

    public class RoomsController : BaseController
    {
        private readonly IRoomsService roomsService;
        private readonly IRoomTypesService roomTypesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RoomsController(
            IRoomsService roomsService,
            IRoomTypesService roomTypesService,
            UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.roomTypesService = roomTypesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();

            var viewModel = new RoomCreateInputModel
            {
                RoomTypes = roomTypes,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RoomCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.SelectedHotelId == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.roomsService.CreateAsync(
                input.Number,
                input.RoomTypeId,
                input.Price,
                input.Description,
                (int)user.SelectedHotelId);

            return this.Redirect("https://localhost:44319");
        }
    }
}
