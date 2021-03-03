namespace MyHotelManager.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AccessDeniedModel : PageModel
    {
        public IActionResult OnGet()
        {
            this.TempData["Message"] = "Error! You do not have access to this resource.";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
