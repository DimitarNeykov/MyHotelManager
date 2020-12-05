namespace MyHotelManager.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        public IActionResult OnGet()
        {
            this.TempData["Message"] = "Please check your email to reset your password.";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
