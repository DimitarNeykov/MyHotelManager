namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Common;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Guests;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
    public class GuestsController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly IGuestsService guestsService;
        private readonly ICountriesService countriesService;

        public GuestsController(
            ICitiesService citiesService,
            IGuestsService guestsService,
            ICountriesService countriesService)
        {
            this.citiesService = citiesService;
            this.guestsService = guestsService;
            this.countriesService = countriesService;
        }

        public IActionResult NewGuest(string reservationId)
        {
            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var countries = this.countriesService.GetAll<CountryDropDownViewModel>();

            var viewModel = new GuestsCreateInputModel
            {
                Cities = cities,
                Countries = countries,
                ReservationId = reservationId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NewGuest(GuestsCreateInputModel input)
        {
            await this.guestsService.CreateAsync(
                input.FirstName,
                input.LastName,
                input.GenderId,
                input.PhoneNumber,
                input.CityId,
                input.CountryId,
                input.UCN,
                input.PNF,
                input.DocumentNumber,
                input.DateOfExpiry,
                input.DateOfIssue,
                input.ReservationId);

            return this.RedirectToAction("Details", "Reservations", new { Id = input.ReservationId });
        }

        public async Task<IActionResult> Update(string guestId)
        {
            var guest = await this.guestsService.GetByIdAsync<GuestUpdateViewModel>(guestId);

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var countries = this.countriesService.GetAll<CountryDropDownViewModel>();

            var viewModel = new GuestUpdateInputModel
            {
                Id = guestId,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                CityId = guest.CityId,
                CountryId = guest.CountryId,
                DateOfExpiry = guest.DateOfExpiry,
                DateOfIssue = guest.DateOfIssue,
                DocumentNumber = guest.DocumentNumber,
                GenderId = guest.GenderId,
                PhoneNumber = guest.PhoneNumber,
                PNF = guest.PNF,
                UCN = guest.UCN,
                Cities = cities,
                Countries = countries,
                ReservationId = guest.ReservationId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GuestUpdateInputModel input)
        {
            if (input.UCN == null && input.PNF == null)
            {
                return this.View(input);
            }

            await this.guestsService.UpdateAsync(
                input.Id,
                input.FirstName,
                input.LastName,
                input.GenderId,
                input.PhoneNumber,
                input.CityId,
                input.CountryId,
                input.UCN,
                input.PNF,
                input.DocumentNumber,
                input.DateOfExpiry,
                input.DateOfIssue);

            return this.RedirectToAction("Details", "Reservations", new { Id = input.ReservationId });
        }
    }
}
