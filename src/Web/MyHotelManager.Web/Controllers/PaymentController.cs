using Microsoft.Extensions.Configuration;

namespace MyHotelManager.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Web.ViewModels.Payments;
    using Stripe;

    public class PaymentController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public PaymentController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Create()
        {

            var user = await this.userManager.GetUserAsync(this.User);
            var userNames = user.FirstName + ' ' + user.LastName;

            var customerService = new CustomerService();
            var customers = await customerService.ListAsync();

            var customer = customers.FirstOrDefault(x => x.Name == userNames);

            if (customer == null)
            {
                var customerOptions = new CustomerCreateOptions
                {
                    Name = userNames,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Metadata = new Dictionary<string, string>
                    {
                        ["HotelId"] = user.HotelId.ToString(),
                        ["UserId"] = user.Id,
                        ["BirthDate"] = user.BirthDate.ToShortDateString(),
                        ["Username"] = user.UserName,
                    },
                };

                customer = await customerService.CreateAsync(customerOptions);
            }

            var priceService = new PriceService();
            var price = await priceService.GetAsync("price_1IP1xzLxakYJAQmTh3ple3eB");

            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = price.UnitAmount,
                Currency = "bgn",
                SetupFutureUsage = "on_session",
                Customer = customer.Id,
            };
            var paymentIntent = await service.CreateAsync(options);

            var model = new PaymentInputModel
            {
                Token = paymentIntent.ClientSecret,
                PaymentId = paymentIntent.Id,
                StripePublishableKey = this.configuration["Stripe:PublishableKey"],
            };

            return this.View(model);
        }

        public IActionResult Success()
        {
            return this.View();
        }

        public IActionResult Cancel()
        {
            return this.View();
        }
    }
}