namespace MyHotelManager.Web.Areas.Administration.Controllers
{
    using MyHotelManager.Common;
    using MyHotelManager.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
