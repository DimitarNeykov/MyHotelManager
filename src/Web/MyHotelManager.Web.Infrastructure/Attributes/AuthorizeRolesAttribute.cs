namespace MyHotelManager.Web.Infrastructure.Attributes
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
            : base()
        {
            this.Roles = string.Join(",", roles);
        }
    }
}
