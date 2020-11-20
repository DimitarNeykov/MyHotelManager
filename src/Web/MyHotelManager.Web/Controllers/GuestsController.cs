namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Guests;

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

        [Authorize]
        public IActionResult NewGuest(string reservationId)
        {
            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var countries = this.countriesService.GetAll<CountryDropDownViewModel>();

            var viewModel = new GuestsCreateInputModel
            {
                Cities = cities,
                Countries = countries,
            };

            return this.View(viewModel);
        }

        [Authorize]
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
    }
}
